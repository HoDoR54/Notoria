namespace NotesBackend.DTOs.Requests
{
    public class UserLogInRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
