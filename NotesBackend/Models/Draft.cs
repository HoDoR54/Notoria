using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class Draft
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<NoteTag> DraftTags { get; set; } = new();
        [Required]
        public Guid NoteId { get; set; }
        public Note Note { get; set; }
    }
}
