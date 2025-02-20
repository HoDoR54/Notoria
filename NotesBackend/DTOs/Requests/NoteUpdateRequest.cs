using NotesBackend.Models;

namespace NotesBackend.DTOs.Requests
{
    public class NoteUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<TagUpdateRequest> Tags { get; set; } = new List<TagUpdateRequest>();
    }
}
