namespace NotesBackend.DTOs.Requests
{
    public class UserPasswordUpdateRequest
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
