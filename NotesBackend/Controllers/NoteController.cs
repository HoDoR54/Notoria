using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;

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

        // get all notes (unarchived)
        [HttpGet("unarchived")]
        public async Task<IActionResult> GetUnarchivedNotes ()
        {
            var notes = await _context.Notes.ToListAsync();
            var activeNotes = notes.Select(n => !n.IsArchived);

            return Ok(activeNotes);
        }

        // get archived notes 
        [HttpGet("archived")]
        public async Task<IActionResult> GetArchivedNotes ()
        {
            var notes = await _context.Notes.ToListAsync();
            var archivedNotes = notes.Select(n => n.IsArchived);

            return Ok(archivedNotes);
        }

        // delete a note by Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteById([FromRoute] Guid id)
        {
            var matchedNote = await _context.Notes.FindAsync(id);
            if (matchedNote == null)
            {
                return NotFound();
            }

            _context.Remove(matchedNote);
            await _context.SaveChangesAsync();

            return Ok("note deleted successfully");
        }

        // archive/unarchive note
        [HttpPut("{id}/{action}")]
        public async Task<IActionResult> ArchiveOrUnarchiveNote([FromRoute] Guid id, [FromRoute] string action)
        {
            var matchedNote = await _context.Notes.FindAsync(id);
            if (matchedNote == null)
            {
                return NotFound();
            }

            bool isArchiving = action.Equals("archive", StringComparison.OrdinalIgnoreCase);

            if (isArchiving && matchedNote.IsArchived)
            {
                return BadRequest("The note is already archived.");
            }

            if (!isArchiving && !matchedNote.IsArchived)
            {
                return BadRequest("The note is not archived.");
            }

            matchedNote.IsArchived = isArchiving;
            await _context.SaveChangesAsync();

            return Ok(isArchiving ? "Note has been archived." : "Note has been unarchived.");
        }

        // create a new note
        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteCreateRequest newNote)
        {
            var response = NoteMappers.FromNewNoteToNoteModel(newNote);

            await _context.AddAsync(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] NoteUpdateRequest updatedNote)
        {
            if (updatedNote == null)
            {
                return BadRequest("Invalid request data.");
            }

            var matchedNote = await _context.Notes.FindAsync(updatedNote.Id);

            if (matchedNote == null)
            {
                return NotFound("Note not found.");
            }

            if (matchedNote.Title == updatedNote.Title && matchedNote.Content == updatedNote.Content)
            {
                return StatusCode(304);
            }

            matchedNote.Title = updatedNote.Title ?? matchedNote.Title;
            matchedNote.Content = updatedNote.Content ?? matchedNote.Content;
            matchedNote.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
