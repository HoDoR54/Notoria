using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;
using System.IdentityModel.Tokens.Jwt;

namespace NotesBackend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly NotesDbContext _context;
        private readonly JwtTokenHelper _jwtHelper;
        public UserController(NotesDbContext context, JwtTokenHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        //User Registration
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Request Data");
            }

            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (matchedUser != null)
            {
                return BadRequest("A user with this email already exists.");
            }

            var mappedUser = UserMappers.RegisterToUser(request);
            string token = _jwtHelper.GenerateToken(mappedUser);

            CookiesHelper.SetTokenCookie(Response, token);

            await _context.Users.AddAsync(mappedUser);
            await _context.SaveChangesAsync();

            var response = UserMappers.ToUserResponse(mappedUser);


            return Ok(response);
        }

        // Log in
        [HttpPost("login")]
        public async Task<IActionResult> LogInAccount([FromBody] UserLogInRequest request)
        {
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u =>u.Email == request.Email);
            if (matchedUser == null)
            {
                return NotFound("No user with this email is found.");
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, matchedUser.Password);

            if (!isPasswordCorrect)
            {
                return BadRequest("Wrong password");
            }

            string token = _jwtHelper.GenerateToken(matchedUser);

            CookiesHelper.SetTokenCookie(Response, token);

            var response = UserMappers.ToUserResponse(matchedUser);

            return Ok(response);
        }

        [HttpGet("auth")]
        public IActionResult ValidateToken()
        {
            try
            {
                var token = Request.Cookies["access_token"];
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { valid = false, message = "No authentication token found" });
                }

                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(token))
                {
                    CookiesHelper.RemoveTokenCookie(Response);
                    return Unauthorized(new { valid = false, message = "Invalid token format" });
                }

                var jwtToken = handler.ReadJwtToken(token);

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    CookiesHelper.RemoveTokenCookie(Response);
                    return Unauthorized(new { valid = false, message = "Token has expired" });
                }

                return Ok(new { valid = true });
            }
            catch (Exception)
            {
                CookiesHelper.RemoveTokenCookie(Response);
                return Unauthorized(new { valid = false, message = "Invalid token" });
            }
        }


        // Delete Account
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcc([FromRoute] Guid id)
        {
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (matchedUser == null)
            {
                return NotFound("No user found");
            }

            _context.Users.Remove(matchedUser);
            await _context.SaveChangesAsync();

            return Ok("Account deleted successfully");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            CookiesHelper.RemoveTokenCookie(Response);
            return Ok(new { message = "Logged out successfully" });
        }

        // Update Account Details
        [HttpPut("update/{updateField}")]
        public async Task<IActionResult> UpdateAcc([FromBody] UserUpdateRequest request, [FromRoute] string memberToUpdate)
        {
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id ==  request.Id);
            if (matchedUser == null)
            {
                return NotFound("User not found");
            }

            string updateParameter = memberToUpdate.ToLower();

            switch (updateParameter)
            {
                case "name":
                    matchedUser.Name = request.Name;
                    break;
                case "email":
                    matchedUser.Email = request.Email;
                    break;
                case "password": 
                    matchedUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    break;
                case "profilePic":
                    matchedUser.ProfilePicUrl = request.ProfilePicUrl;
                    break;
                default:
                    return BadRequest("Invalid parameter.");
            }

            matchedUser.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok($"{updateParameter} updated succcessfully");
        }
    }
}
