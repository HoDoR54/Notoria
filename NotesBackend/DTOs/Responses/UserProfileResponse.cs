using Newtonsoft.Json.Converters;
using NotesBackend.Models;

namespace NotesBackend.DTOs.Responses
{
    public class UserProfileResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? ProfilePicUrl { get; set; }
    }
}
