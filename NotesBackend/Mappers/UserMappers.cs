using Microsoft.AspNetCore.Identity;
using NotesBackend.DTOs.Requests;
using NotesBackend.DTOs.Responses;
using NotesBackend.Models;

namespace NotesBackend.Mappers
{
    public static class UserMappers
    {
        public static UserSignUpResponse ToUserRegisResponse (User user)
        {
            UserSignUpResponse response = new UserSignUpResponse ();

            response.Id = user.Id;
            response.Name = user.Name;
            response.Email = user.Email;


            return response;
        }

        public static UserLogInResponse ToUserLogInResponse (User user)
        {
            UserLogInResponse response = new UserLogInResponse ();

            response.Id = user.Id;
            response.Name = user.Name;
            response.Email = user.Email;
            response.CreatedAt = user.CreatedAt;
            response.ProfilePicUrl = user.ProfilePicUrl;

            return response;
        }

        public static User ToUser (UserSignUpRequest newUser)
        {
            User user = new User ();
            user.Name = newUser.Name;
            user.Email = newUser.Email;
            user.Password = newUser.Password;

            return user;
        }
    }
}
