using NotesBackend.Models;

namespace NotesBackend.DTOs.Responses
{
    public class UserSignUpResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } =string.Empty;
    }
}
