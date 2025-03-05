using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Data;
using NotesBackend.DTOs.Requests;
using NotesBackend.Helpers;
using NotesBackend.Models;
using System.IdentityModel.Tokens.Jwt;

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

            // check if the email is already in use
            var matchedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (matchedEmail != null)
            {
                return BadRequest("A user with this email already exists.");
            }

            // validate data
            if (!ValidationHelper.ValidateEmail(request.Email) || !ValidationHelper.ValidatePassword(request.Password))
            {
                return BadRequest("Invalid Request Data");
            }

            // map the request to a user object
            var mappedUser = UserMappers.RegisterToUser(request);

            // add the user to the database
            await _context.Users.AddAsync(mappedUser);
            await _context.SaveChangesAsync();

            // map the user to a response object
            var response = UserMappers.ToUserResponse(mappedUser);

            // generate tokens
            string accessToken = _jwtHelper.GenerateToken(mappedUser, true);
            string refreshToken = _jwtHelper.GenerateToken(mappedUser, false);

            // add the refresh token to the database
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = mappedUser.Id
            });
            await _context.SaveChangesAsync();

            // set the cookies
            CookiesHelper.SetTokenCookie(HttpContext.Response, accessToken, true);
            CookiesHelper.SetTokenCookie(HttpContext.Response, refreshToken, false);

            return Ok(response);
        }

        // Log in
        [HttpPost("login")]
        public async Task<IActionResult> LogInAccount([FromBody] UserLogInRequest request)
        {
            // check if the user with the given email exists
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

            // map the user to a response object
            var response = UserMappers.ToUserResponse(matchedUser);

            // generate tokens
            string accessToken = _jwtHelper.GenerateToken(matchedUser, true);
            string refreshToken = _jwtHelper.GenerateToken(matchedUser, false);
            
            // add the refresh token to the database
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = matchedUser.Id
            });
            await _context.SaveChangesAsync();

            // set the cookies
            CookiesHelper.SetTokenCookie(HttpContext.Response, accessToken, true);
            CookiesHelper.SetTokenCookie(HttpContext.Response, refreshToken, false);

            return Ok(response);
        }

        // Authenticate
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser()
        {
            // get the access token from the request cookies
            var accessCookie = HttpContext.Request.Cookies["access_token"];
            if (accessCookie == null)
            {
                return Unauthorized(new { valid = false, message = "No access token found" });
            }

            // check if the token is valid
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(accessCookie))
            {
                return Unauthorized(new { valid = false, message = "Access token is invalid" });
            }

            // check if the token is expired
            if (DateTime.UtcNow > handler.ReadJwtToken(accessCookie).ValidTo)
            {
                return Unauthorized(new { valid = false, message = "Expired token" });
            }

            // get the refresh token from the request cookies and check if it exists
            var refreshCookie = HttpContext.Request.Cookies["refresh_token"];
            var matchedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshCookie);

            if (matchedToken == null)
            {
                return Unauthorized(new { valid = false, message = "No refresh token found" });
            }

            // get the associated user from the database and map it to a response object
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == matchedToken.UserId);
            var response = UserMappers.ToUserResponse(matchedUser);

            return Ok(new { valid = true, message = "Authenticated", user = response });
        }

        // Refresh access-token using refresh-token
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            // get both tokens from the request cookies
            var accessCookie = HttpContext.Request.Cookies["access_token"];
            var refreshCookie = HttpContext.Request.Cookies["refresh_token"];

            // check request validity
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

            // check if the refresh token is expired and prompt to trigger a log-in if it is
            if (DateTime.UtcNow > handler.ReadJwtToken(refreshCookie).ValidTo)
            {
                return Unauthorized(new { valid = false, message = "Refresh token is expired. Trigger a log-in" });
            }

            var matchedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshCookie);
            if (matchedRefreshToken == null)
            {
                return Unauthorized(new { valid = false, message = "Refresh token is invalid" });
            }

            // generate a new access token and set it in the cookies
            var newAccessToken = _jwtHelper.GenerateToken(matchedRefreshToken.User, true);
            CookiesHelper.SetTokenCookie(HttpContext.Response, newAccessToken, true);

            if (matchedRefreshToken.IsRevoked == false)
            {
                matchedRefreshToken.IsRevoked = true;
            }
            await _context.SaveChangesAsync();

            return Ok(new { valid = true, message = "Access token refreshed" });
        }

        // Log out
        [HttpPost("logout")]
        public async Task<IActionResult> LogOutUser()
        {
            // get the refresh token from the request cookies and get the associated user
            var refreshCookie = HttpContext.Request.Cookies["refresh_token"];
            var matchedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshCookie);
            if (matchedToken == null)
            {
                return Unauthorized(new { valid = false, message = "No refresh token found" });
            }
            var matchedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == matchedToken.UserId);
            if (matchedUser == null)
            {
                return NotFound("No user found");
            }

            // delete the user from the database
            _context.RefreshTokens.Remove(matchedToken);
            await _context.SaveChangesAsync();

            return Ok("Logged out successfully");
        }

        // Delete Account
        [HttpDelete("id")]
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
