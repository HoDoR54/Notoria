using System;
using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TagName {  get; set; } = string.Empty;

        [Required]
        public List<NoteTag> NoteTags { get; set; }
    }
}
