using NotesBackend.Models;

namespace NotesBackend.DTOs.Requests
{
    public class NoteCreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<TagCreateRequest> Tags { get; set; } = [];
        public Guid UserId { get; set; }
    }
}
