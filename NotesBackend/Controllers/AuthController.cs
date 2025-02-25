using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using NotesBackend.DTOs.Responses;


namespace NotesBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NotesDbContext _context;
        private readonly JwtTokenHelper _jwtHelper;
        public AuthController(NotesDbContext context, JwtTokenHelper jwtHelper, CookiesHelper cookieHelper)
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

            var matchedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (matchedEmail != null)
            {
                return BadRequest("A user with this email already exists.");
            }

            var mappedUser = UserMappers.RegisterToUser(request);

            await _context.Users.AddAsync(mappedUser);
            await _context.SaveChangesAsync();

            var response = UserMappers.ToUserResponse(mappedUser);

            string accessToken = _jwtHelper.GenerateToken(mappedUser, true);
            string refreshToken = _jwtHelper.GenerateToken(mappedUser, false);

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = mappedUser.Id
            });
            await _context.SaveChangesAsync();

            CookiesHelper.SetTokenCookie(HttpContext.Response, accessToken, true);
            CookiesHelper.SetTokenCookie(HttpContext.Response, refreshToken, false);

            return Ok(response);
        }

        // Log in
        [HttpPost("login")]
        public async Task<IActionResult> LogInAccount([FromBody] UserLogInRequest request)
        {
            // Check email duplication
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (matchedUser == null)
            {
                return NotFound("No user with this email is found.");
            }

            // Check password correction
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(request.Password, matchedUser.Password);
            if (!isPasswordCorrect)
            {
                return BadRequest("Wrong password");
            }

            var response = UserMappers.ToUserResponse(matchedUser);

            string accessToken = _jwtHelper.GenerateToken(matchedUser, true);
            string refreshToken = _jwtHelper.GenerateToken(matchedUser, false);
            
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = matchedUser.Id
            });

            await _context.SaveChangesAsync();

            CookiesHelper.SetTokenCookie(HttpContext.Response, accessToken, true);
            CookiesHelper.SetTokenCookie(HttpContext.Response, refreshToken, false);

            return Ok(response);
        }

        // Authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser()
        {
            var accessCookie = HttpContext.Request.Cookies["access_token"];
            if (accessCookie == null)
            {
                return Unauthorized(new { valid = false, message = "No access token found" });
            }

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(accessCookie))
            {
                return Unauthorized(new { valid = false, message = "Access token is invalid" });
            }

            if (DateTime.UtcNow > handler.ReadJwtToken(accessCookie).ValidTo)
            {
                return Unauthorized(new { valid = false, message = "Expired token" });
            }

            var refreshCookie = HttpContext.Request.Cookies["refresh_token"];
            var matchedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshCookie);

            if (matchedToken == null)
            {
                return Unauthorized(new { valid = false, message = "No refresh token found" });
            }
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == matchedToken.UserId);
            var response = UserMappers.ToUserResponse(matchedUser);

            return Ok(new { valid = true, message = "Authenticated", user = response });
        }

        // Refresh access-token
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            var accessCookie = HttpContext.Request.Cookies["access_token"];
            var refreshCookie = HttpContext.Request.Cookies["refresh_token"];

            if (accessCookie == null)
            {
                return Unauthorized(new { valid = false, message = "No access token found" });
            }

            if (refreshCookie == null)
            {
                return Unauthorized(new { valid = false, message = "No refresh token found" });
            }

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(refreshCookie))
            {
                return Unauthorized(new { valid = false, message = "Refresh token is invalid" });
            }

            if (!handler.CanReadToken(accessCookie))
            {
                return Unauthorized(new { valid = false, message = "Access token is invalid" });
            }

            if (DateTime.UtcNow > handler.ReadJwtToken(refreshCookie).ValidTo)
            {
                return Unauthorized(new { valid = false, message = "Refresh token is expired. Trigger a log-in" });
            }

            var matchedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshCookie);
            if (matchedRefreshToken == null)
            {
                return Unauthorized(new { valid = false, message = "Refresh token is invalid" });
            }

            var newAccessToken = _jwtHelper.GenerateToken(matchedRefreshToken.User, true);
            CookiesHelper.SetTokenCookie(HttpContext.Response, newAccessToken, true);

            if (matchedRefreshToken.IsRevoked == false)
            {
                matchedRefreshToken.IsRevoked = true;
            }
            await _context.SaveChangesAsync();

            return Ok(new { valid = true, message = "Access token refreshed" });
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
    }
}
