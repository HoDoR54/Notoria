using NotesBackend.DTOs.Requests;
using NotesBackend.Models;

namespace NotesBackend.Helpers
{
    public static class TagMappers
    {
        public static Tag FromNewTagToTagModel (TagCreateRequest newNote)
        {
            Tag tag = new Tag ();
            tag.TagName = newNote.TagName;
            return tag;
        }
    }
}
