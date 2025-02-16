using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class NoteTag
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid NoteId { get; set; }

        public Note Note { get; set; }

        [Required]
        public Guid TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
