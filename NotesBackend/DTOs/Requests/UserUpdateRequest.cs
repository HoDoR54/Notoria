﻿namespace NotesBackend.DTOs.Requests
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? ProfilePicUrl { get; set; }
    }
}
