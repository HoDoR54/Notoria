using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;

namespace NotesBackend.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Request Data");
            }

            

            var mappedUser = UserMappers.RegisterToUser(request);
            string token = _jwtHelper.GenerateToken(mappedUser);
            await _context.Users.AddAsync(mappedUser);
            await _context.SaveChangesAsync();

            var response = UserMappers.UserToRegisResponse(mappedUser);
            response.Token = token;


            return Ok(response);
        }
    }
}
