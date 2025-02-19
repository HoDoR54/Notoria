using Microsoft.AspNetCore.Mvc;
using NotesBackend.Models;
using NotesBackend.Data;
using NotesBackend.Helpers;
using NotesBackend.DTOs.Requests;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace NotesBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NotesDbContext _context;
        private readonly JwtTokenHelper _jwtHelper;
        public UsersController(NotesDbContext context, JwtTokenHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        // get user info as page loads
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfo([FromRoute] Guid id)
        { 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var response = UserMappers.ToPageLoadResponse(user);

            return Ok(response);
        }

        // user registration
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserSignUpRequest newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }

            User user = UserMappers.ToUser(newUser);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = _jwtHelper.GenerateToken(user);

            var response = UserMappers.ToUserRegisResponse(user);
            response.Token = token;

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogIn(UserLogInRequest user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var matchedUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (matchedUser == null || matchedUser.Password != user.Password)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = _jwtHelper.GenerateToken(matchedUser);

            var response = UserMappers.ToUserLogInResponse(matchedUser);
            response.Token = token;

            return Ok(response);
        }


        //update user details
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserAccountUpdateRequest updatedProfile)
        {
            if (updatedProfile == null)
            {
                return BadRequest("Invalid request.");
            }

            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedProfile.Id);

            if (matchedUser == null)
            {
                return NotFound("User not found.");
            }

            bool isUpdated = false;

            if (!string.IsNullOrWhiteSpace(updatedProfile.Email) && matchedUser.Email != updatedProfile.Email)
            {
                matchedUser.Email = updatedProfile.Email;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updatedProfile.Name) && matchedUser.Name != updatedProfile.Name)
            {
                matchedUser.Name = updatedProfile.Name;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updatedProfile.ProfilePicUrl) && matchedUser.ProfilePicUrl != updatedProfile.ProfilePicUrl)
            {
                matchedUser.ProfilePicUrl = updatedProfile.ProfilePicUrl;
                isUpdated = true;
            }

            if (!string.IsNullOrWhiteSpace(updatedProfile.Password))
            {
                matchedUser.Password = updatedProfile.Password;
                isUpdated = true;
            }

            if (isUpdated)
            {
                matchedUser.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return Ok("Profile updated successfully.");
            }

            return BadRequest("No changes were made.");
        }

        // delete account
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcc([FromRoute] Guid id)
        {
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (matchedUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(matchedUser);
            await _context.SaveChangesAsync();

            return Ok("Account deleted Successfully");
        }
    }
}
