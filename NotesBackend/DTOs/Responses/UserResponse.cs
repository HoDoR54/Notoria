
using NotesBackend.Models;

namespace NotesBackend.DTOs.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Note> Notes { get; set; } = [];
        public Preference? Preference { get; set; } = new Preference();
    }
}
