namespace NotesBackend.Helpers
{
    public class CookiesHelper
    {
        public static CookieOptions GetCookieOptions(bool isAccessToken)
        {
            DateTime expiresAt = isAccessToken ? DateTime.UtcNow.AddMinutes(15) : DateTime.UtcNow.AddDays(7);
            TimeSpan maxAge = isAccessToken ? TimeSpan.FromMinutes(15) : TimeSpan.FromDays(7);
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expiresAt,
                MaxAge = maxAge,
                IsEssential = true
            };
        }

        public static void SetTokenCookie(HttpResponse response, string token, bool isAccessToken)
        {
            response.Cookies.Append(isAccessToken ? "access_token": "refresh_token", token, GetCookieOptions(isAccessToken));
        }

        public static void RemoveTokenCookie(HttpResponse response, bool isAccessToken)
        {
            response.Cookies.Delete(isAccessToken ? "access_token": "refresh_token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/"
            });
        }
    }
}
