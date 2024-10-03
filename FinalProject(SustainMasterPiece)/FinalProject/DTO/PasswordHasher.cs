namespace FinalProject.DTO
{
    public class PasswordHasher
    {
        //function Register
        public static void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key; // The Key property provides a randomly generated salt.
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        // function Login
        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHash1 = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return passwordHash1.SequenceEqual(passwordHash);
            }
        }
    }
}
