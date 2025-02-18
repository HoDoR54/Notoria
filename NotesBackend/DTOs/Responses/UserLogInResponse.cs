
using Microsoft.AspNetCore.Identity;

namespace NotesBackend.DTOs.Responses
{
    public class UserLogInResponse
    {
        public string Token { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfilePicUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
