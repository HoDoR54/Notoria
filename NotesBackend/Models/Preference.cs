﻿using System.ComponentModel.DataAnnotations;

namespace NotesBackend.Models
{
    public enum Theme
    {
        Light,
        Dark,
        Happy,
        Sad
    }

    public enum FontFam
    {
        TimesNewRoman,
        Arial,
        Garamond
    }

    public class Preference
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Theme Theme { get; set; } = Theme.Dark;

        public FontFam FontFam { get; set; } = FontFam.Arial;

        public int? FontSize { get; set; }
    }
}
