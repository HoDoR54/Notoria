using NotesBackend.DTOs.Requests;
using NotesBackend.DTOs.Responses;
using NotesBackend.Models;

namespace NotesBackend.Helpers
{
    public class NoteMappers
    {
        public static Note NewNoteToNoteModel(NoteCreateRequest request)
        {
            Note note = new Note
            {
                Title = request.Title,
                Content = request.Content,
                UserId = request.UserId,
            };
            return note;
        }

        public static NoteCreateResponse ToNewNoteResponse(Note request)
        {
            NoteCreateResponse note = new NoteCreateResponse
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
                IsArchived = request.IsArchived,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
            };
            return note;
        }

        public static NoteResponse ToNoteResponse (Note request)
        {

           NoteResponse note = new NoteResponse
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                IsArchived = request.IsArchived,
            };
            return note;
        }
    }
}
