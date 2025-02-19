using NotesBackend.DTOs.Requests;
using NotesBackend.Models;

namespace NotesBackend.Helpers
{
    public static class NoteMappers
    {
        public static Note FromNewNoteToNoteModel (NoteCreateRequest newNote)
        {
            Note note = new Note();

            note.Title = newNote.Title;

            return note;
        }
    }
}
