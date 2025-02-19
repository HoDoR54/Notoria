using Microsoft.AspNetCore.Identity;
using NotesBackend.DTOs.Requests;
using NotesBackend.DTOs.Responses;
using NotesBackend.Models;

namespace NotesBackend.Helpers
{
    public static class UserMappers
    {
        public static UserSignUpResponse ToUserRegisResponse(User user)
        {
            UserSignUpResponse response = new UserSignUpResponse();

            response.Id = user.Id;
            response.Name = user.Name;
            response.Email = user.Email;


            return response;
        }

        public static UserLogInResponse ToUserLogInResponse(User user)
        {
            UserLogInResponse response = new UserLogInResponse();

            response.Id = user.Id;
            response.Name = user.Name;
            response.Email = user.Email;
            response.CreatedAt = user.CreatedAt;
            response.ProfilePicUrl = user.ProfilePicUrl;

            return response;
        }

        public static User ToUser(UserSignUpRequest newUser)
        {
            User user = new User();
            user.Name = newUser.Name;
            user.Email = newUser.Email;
            user.Password = newUser.Password;

            return user;
        }

        public static UserProfileResponse ToProfileResponse(User user)
        {
            UserProfileResponse response = new UserProfileResponse();

            response.Id = user.Id;
            response.Name = user.Name;
            response.Email = user.Email;
            response.ProfilePicUrl = user.ProfilePicUrl;

            return response;
        }

        public static UserPageLoadResponse ToPageLoadResponse(User user)
        {
            UserPageLoadResponse response = new UserPageLoadResponse();

            response.Id = user.Id;
            response.Name = user.Name;
            response.Email = user.Email;
            response.Notes = user.Notes;
            response.CreatedAt = user.CreatedAt;
            response.UpdatedAt = user.UpdatedAt;
            response.ProfilePicUrl = user.ProfilePicUrl;
            response.Preference = user.Preference;

            return response;
        }
    }
}
