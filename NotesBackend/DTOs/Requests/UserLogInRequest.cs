using System.ComponentModel.DataAnnotations;

namespace NotesBackend.DTOs.Requests
{
    public class UserLogInRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Token {  get; set; } = string.Empty;
    }
}
