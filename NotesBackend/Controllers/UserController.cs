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
