namespace NotesBackend.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidateEmail (string email)
        {
            if (email == null)
            {
                return false;
            }
            if (email.Length < 5)
            {
                return false;
            }
            return true;
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 20)
            {
                return false;
            }

            bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsLetterOrDigit(c)) hasSpecial = true;

                // Stop early if all the conditions turn true
                if (hasUpper && hasLower && hasDigit && hasSpecial)
                {
                    return true;
                }
            }

            return false;
        }

    }
}