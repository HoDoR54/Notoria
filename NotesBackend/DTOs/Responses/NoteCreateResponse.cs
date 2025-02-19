using NotesBackend.Models;

namespace NotesBackend.DTOs.Responses
{
    public class NoteCreateResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<NoteTag> NoteTags { get; set; } = [];
        public Guid? DraftId { get; set; }
        public Draft? Draft { get; set; }
        public Guid UserId { get; set; }
        public UserResponse User { get; set; }
    }
}
