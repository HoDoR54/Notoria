namespace NotesBackend.Helpers
{
    public class CookiesHelper
    {
        public static CookieOptions GetSecureTokenCookieOptions()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
                MaxAge = TimeSpan.FromDays(7),
                IsEssential = true
            };
        }

        public static void SetTokenCookie(HttpResponse response, string token)
        {
            response.Cookies.Append("access_token", token, GetSecureTokenCookieOptions());
        }

        public static void RemoveTokenCookie(HttpResponse response)
        {
            response.Cookies.Delete("access_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/"
            });
        }
    }
}
