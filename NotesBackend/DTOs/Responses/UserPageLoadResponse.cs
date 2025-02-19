using NotesBackend.Models;

namespace NotesBackend.DTOs.Responses
{
    public class UserPageLoadResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? ProfilePicUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public List<Note>? Notes { get; set; } = new List<Note>();

        public Preference? Preference { get; set; } = new Preference();
    }
}
