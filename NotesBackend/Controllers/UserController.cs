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

        // update account name
        [HttpPut("{id}/name")]
        public async Task<IActionResult> UpdateName([FromRoute] Guid id, [FromBody] UserNameUpdateRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Wrong password");
            }

            user.Name = request.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // update account mail
        [HttpPut("{id}/mail")]
        public async Task<IActionResult> UpdateMail([FromRoute] Guid id, [FromBody] UserMailUpdateRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Wrong password");
            }

            if (!ValidationHelper.ValidateEmail(request.Email))
            {
                return BadRequest("Invalid email");
            }

            user.Email = request.Email;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // update account password
        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid id, [FromBody] UserPasswordUpdateRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return BadRequest("Wrong password");
            }

            if (!ValidationHelper.ValidatePassword(request.NewPassword))
            {
                return BadRequest("Invalid password");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
