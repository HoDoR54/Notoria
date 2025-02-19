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

            _context.NoteTags.AddRange(noteTags);
            await _context.SaveChangesAsync();

            var response = NoteMappers.ToNewNoteResponse(mappedNote);
            return Ok(response);
        }

    }
}
