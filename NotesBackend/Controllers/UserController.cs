using Microsoft.AspNetCore.Mvc;
using NotesBackend.Models;
using NotesBackend.Data;
using NotesBackend.Mappers;
using NotesBackend.DTOs.Requests;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace NotesBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NotesDbContext _context;
        private readonly string _secretKey;
        private readonly JwtTokenHelper _jwtHelper; 
        public UsersController(NotesDbContext context, JwtTokenHelper jwtHelper)
        {
            _context = context;
            _secretKey = jwtHelper.GetSecretKey();
            _jwtHelper = jwtHelper;
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


    }
}
