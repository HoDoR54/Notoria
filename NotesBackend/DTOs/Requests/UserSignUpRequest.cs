using System.ComponentModel.DataAnnotations;

namespace NotesBackend.DTOs.Requests
{
    public class UserSignUpRequest
    {
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        public string? ProfilePicUrl { get; set; }
    }
}
