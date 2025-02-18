using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        public string? ProfilePicUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public List<Note>? Notes { get; set; } = new List<Note>();

        public Preference? Preference { get; set; } = new Preference();
    }
}
