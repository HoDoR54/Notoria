using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;

namespace NotesBackend.Controllers
{
    [Route("api/drafts")]
    [ApiController]
    public class DraftController : ControllerBase
    {
        private readonly NotesDbContext _context;
        public DraftController(NotesDbContext context)
        {
            _context = context;
        }

        // create a draft (draft controller)
        [HttpPost]
        public async Task<IActionResult> AddNewDraft([FromBody] Note request)
        {
            // map the request to a new draft and save to the database
            var mappedDraft = DraftMappers.NewDraftToDraftModel(request);
            request.NoteTags.ForEach(tag => mappedDraft.DraftTags.Add(new NoteTag { TagId = tag.Id, NoteId = mappedDraft.Id }));
            await _context.SaveChangesAsync();
            return Ok(NoteMappers.ToNoteResponse(request));
        }

        // get all drafts (draft controller)
        [HttpGet]
        public async Task<IActionResult> GetAllDrafts()
        {
            // get all drafts from the database
            var drafts = await _context.Drafts.ToListAsync();
            return Ok(drafts.Select(d => NoteMappers.ToNoteResponse(DraftMappers.DraftToNoteModel(d))));
        }

        // delete the draft and save it as a note (draft controller)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDraft(Guid id)
        {
            // get the draft from the database
            var draft = await _context.Drafts.FirstOrDefaultAsync(d => d.Id == id);
            if (draft == null)
            {
                return NotFound("No matched draft found");
            }

            // merge the draft to a note and save to the database
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == draft.NoteId);
            note.Title = draft.Title;
            note.Content = draft.Content;
            note.NoteTags = draft.DraftTags;
            note.UpdatedAt  = DateTime.UtcNow;

            // remove the draft from the database
            _context.Drafts.Remove(draft);
            await _context.SaveChangesAsync();

            return Ok(NoteMappers.ToNoteResponse(note));
        }
    }
}
