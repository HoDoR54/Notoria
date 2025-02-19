using NotesBackend.DTOs.Requests;
using NotesBackend.Models;
using BCrypt.Net;
using NotesBackend.DTOs.Responses;

namespace NotesBackend.Helpers
{
    public class UserMappers
    {
        public static User RegisterToUser (UserRegisterRequest request)
        {

            User mappedUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                ProfilePicUrl = request.ProfilePicUrl,
            };
            return mappedUser;
        }

        public static UserRegisterResponse UserToRegisResponse (User user)
        {
            UserRegisterResponse response = new UserRegisterResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Preference = user.Preference,
                Notes = user.Notes,
            };
            return response;
        }
    }
}
