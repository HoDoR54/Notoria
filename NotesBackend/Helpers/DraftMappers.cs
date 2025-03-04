using NotesBackend.Models;

namespace NotesBackend.Helpers
{
    public class DraftMappers
    {
        public static Draft NewDraftToDraftModel(Note request)
        {
            Draft draft = new Draft
            {
                Title = request.Title,
                Content = request.Content,
                NoteId = request.Id,
            };
            return draft;
        }

        public static Note DraftToNoteModel(Draft draft)
        {
            Note note = new Note
            {
                Title = draft.Title,
                Content = draft.Content,
                UserId = draft.Note.UserId,
            };
            return note;
        }
    }
}
