using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesBackend.Models
{
    public class NoteTag
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Note")]
        public Guid NoteId { get; set; }

        public Note? Note { get; set; }

        [Required]
        [ForeignKey("Tag")]
        public Guid TagId { get; set; }

        public Tag? Tag { get; set; }
    }
}
