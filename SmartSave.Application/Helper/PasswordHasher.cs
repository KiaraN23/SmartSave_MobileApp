namespace SmartSave.Application.Helper
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool Verify(string passwordRequest, string existingPassword)
            => BCrypt.Net.BCrypt.Verify(passwordRequest, existingPassword);
    }
}
