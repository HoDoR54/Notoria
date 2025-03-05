namespace NotesBackend.DTOs.Requests
{
    public class UserMailUpdateRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
