using System.Net.Mail;

namespace SmartSave.Application.Helper
{
    public static class ValidatorHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return mail.Address.Equals(email);
            } catch (Exception)
            {
                return false;
            }
        }

        public static bool IsStrongPassword(string password)
        {
            return password.Length >= 8
                && password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit);
        }
    }
}
