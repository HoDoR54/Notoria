using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;
using System.Linq.Expressions;

namespace NotesBackend.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NotesDbContext _context;
        public NoteController(NotesDbContext context)
        {
            _context = context;
        }

        // create a new note including an array of tags
        [HttpPost]
        public async Task<IActionResult> AddNewNote([FromBody] NoteCreateRequest request)
        {
            var mappedNote = NoteMappers.NewNoteToNoteModel(request);
            _context.Notes.Add(mappedNote);
            await _context.SaveChangesAsync(); // save to the database so that it can be accessed later

            var existingTags = await _context.Tags
                .Where(t => request.Tags.Select(rt => rt.TagName.ToLower()).Contains(t.TagName.ToLower()))
                .ToListAsync();

            var newTags = request.Tags
                .Where(rt => !existingTags.Any(et => et.TagName.ToLower() == rt.TagName.ToLower()))
                .Select(rt => new Tag { TagName = rt.TagName })
                .ToList();

            if (newTags.Any())
            {
                _context.Tags.AddRange(newTags);
                await _context.SaveChangesAsync();
            }

            var allTags = existingTags.Concat(newTags).ToList();

            var noteTags = allTags.Select(tag => new NoteTag
            {
                NoteId = mappedNote.Id,
                TagId = tag.Id
            }).ToList();
            
            foreach (var tag in noteTags)
            {
                mappedNote.NoteTags.Add(tag);
            }
            _context.NoteTags.AddRange(noteTags);
            await _context.SaveChangesAsync();

            var response = NoteMappers.ToNewNoteResponse(mappedNote);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> SaveNoteChanges([FromBody] NoteUpdateRequest request)
        {
            var matchedNote = await _context.Notes
                .Include(n => n.NoteTags)
                .ThenInclude(nt => nt.Tag)
                .FirstOrDefaultAsync(n => n.Id == request.Id);

            if (matchedNote == null)
            {
                return NotFound("No note with this id is found");
            }

            matchedNote.Title = request.Title;
            matchedNote.Content = request.Content;

            // Get existing tags from the database
            var existingTags = await _context.Tags
                .Where(t => request.Tags.Select(rt => rt.TagName.ToLower()).Contains(t.TagName.ToLower()))
                .ToListAsync();

            // Find new tags that need to be created
            var newTags = request.Tags
                .Where(rt => !existingTags.Any(et => et.TagName.ToLower() == rt.TagName.ToLower()))
                .Select(rt => new Tag { TagName = rt.TagName })
                .ToList();

            if (newTags.Any())
            {
                _context.Tags.AddRange(newTags);
                await _context.SaveChangesAsync();
                existingTags.AddRange(newTags); // Ensure new tags are included
            }

            // Decide which tags to remove
            var requestTagNames = request.Tags.Select(rt => rt.TagName.ToLower()).ToHashSet();
            var noteTagsToRemove = matchedNote.NoteTags
                .Where(nt => !requestTagNames.Contains(nt.Tag.TagName.ToLower()))
                .ToList();

            if (noteTagsToRemove.Any())
            {
                _context.NoteTags.RemoveRange(noteTagsToRemove);
            }

            // Find which tags are missing from the current note
            var currentTagNames = matchedNote.NoteTags.Select(nt => nt.Tag.TagName.ToLower()).ToHashSet();
            var noteTagsToAdd = existingTags
                .Where(tag => !currentTagNames.Contains(tag.TagName.ToLower()))
                .Select(tag => new NoteTag
                {
                    NoteId = matchedNote.Id,
                    TagId = tag.Id
                })
                .ToList();

            if (noteTagsToAdd.Any())
            {
                _context.NoteTags.AddRange(noteTagsToAdd);
            }

            matchedNote.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // archive or unarchive a note
        [HttpPut("archive/{isArchiving}/{noteId}")]
        public async Task<IActionResult> ArchiveOrUnarchive([FromRoute] bool isArchiving, Guid noteId)
        {
            var matchedNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId);
            if (matchedNote == null)
            {
                return NotFound("No note with this ID was found.");
            }

            if (isArchiving == matchedNote.IsArchived)
            {
                return BadRequest(isArchiving ? "Note is already archived." : "Note is already unarchived.");
            }

            matchedNote.IsArchived = isArchiving;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
