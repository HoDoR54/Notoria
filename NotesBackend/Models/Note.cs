using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }  = string.Empty;

        public string Content { get; set; } = string.Empty;

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public ICollection<NoteTag> NoteTags { get; set; }

        public Draft? Draft { get; set; }
    }
}
