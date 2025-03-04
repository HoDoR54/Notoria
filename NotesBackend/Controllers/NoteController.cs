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
            response.Tags = allTags.Select(at => new DTOs.Responses.TagResponse { Id = at.Id, TagName = at.TagName }).ToList();
            return Ok(response);
        }

        // get all notes
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _context.Notes.ToListAsync();

            var response = notes.Select(n => NoteMappers.ToNoteResponse(n)).ToList();
            response.ForEach(r => r.Tags = _context.NoteTags
                .Where(nt => nt.NoteId == r.Id)
                .Select(nt => nt.Tag)
                .Select(t => new DTOs.Responses.TagResponse { Id = t.Id, TagName = t.TagName })
                .ToList());
            return Ok(response);
        }

        // archive/unarchive a note
        [HttpPatch("{id}")]
        public async Task<IActionResult> ArchiveUnarchiveNote([FromBody] Guid noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if ( note == null)
            {
                return NotFound("No matched note found.");             
            }
            note.IsArchived = !note.IsArchived;
            await _context.SaveChangesAsync();
            return Ok(NoteMappers.ToNoteResponse(note));
        }
        
        // delete a note
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote ([FromBody] Guid noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound("No matched note found.");
            }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return Ok("Note deleted successfully.");
        }

        // add a tag to a note
        [HttpPost("{id}/tags/add")]
        public async Task<IActionResult> AddTag([FromBody] TagCreateRequest newTag, [FromRoute] Guid noteId)
        {
            // find matched note
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound("No matched note found.");
            }

            // check if the tag already exists
            var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName.ToLower() == newTag.TagName.ToLower());
            if (existingTag == null)
            {
                var tag = new Tag { TagName = newTag.TagName };
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                existingTag = tag;
            }
            
            // add the note-tag connection to the database
            var notTag = new NoteTag { NoteId = noteId, TagId = existingTag.Id };
            _context.NoteTags.Add(notTag);
            await _context.SaveChangesAsync();

            return Ok(NoteMappers.ToNoteResponse(note));
        }

        // remove a tag from a note
        [HttpDelete("{id}/tags/remove")]
        public async Task<IActionResult> RemoveTag([FromBody] TagCreateRequest tag, [FromRoute] Guid noteId)
        {
            // find matched note
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound("No matched note found.");
            }

            // check if the tag already exists
            var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName.ToLower() == tag.TagName.ToLower());
            if (existingTag == null)
            {
                return NotFound("No matched tag found.");
            }

            // remove the note-tag connection from the database
            var noteTag = await _context.NoteTags.FirstOrDefaultAsync(nt => nt.NoteId == noteId && nt.TagId == existingTag.Id);
            if (noteTag == null)
            {
                return BadRequest("No matched note-tag connection found.");
            }
            _context.NoteTags.Remove(noteTag);
            await _context.SaveChangesAsync();

            return Ok(NoteMappers.ToNoteResponse(note));
        }

        // get a note by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById([FromRoute] Guid noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound("No matched note found.");
            }

            var response = NoteMappers.ToNoteResponse(note);
            response.Tags = _context.NoteTags
                .Where(nt => nt.NoteId == noteId)
                .Select(nt => nt.Tag)
                .Select(t => new DTOs.Responses.TagResponse { Id = t.Id, TagName = t.TagName })
                .ToList();
            return Ok(response);
        }

        // update a note title
        [HttpPatch("{id}/title")]
        public async Task<IActionResult> UpdateNoteTitle([FromBody] NoteCreateRequest request, [FromRoute] Guid noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound("No matched note found.");
            }

            note.Title = request.Title;
            await _context.SaveChangesAsync();
            return Ok(NoteMappers.ToNoteResponse(note));
        }

        // update a note content
        [HttpPatch("{id}/content")]
        public async Task<IActionResult> UpdateNoteContent([FromBody] NoteCreateRequest request, [FromRoute] Guid noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note == null)
            {
                return NotFound("No matched note found.");
            }

            note.Content = request.Content;
            await _context.SaveChangesAsync();
            return Ok(NoteMappers.ToNoteResponse(note));
        }

        // create a draft (draft controller)        
        // get all drafts (draft controller)
        // delete the draft and save it as a note (draft controller)

    }
}
