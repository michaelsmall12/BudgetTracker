using System.Security.Cryptography;
using System.Text;

namespace BudgetTracker.Entites
{
    public class User
    {
        private const int SaltSize = 16; // 16 bytes salt
        private const int HashSize = 32; // 32 bytes hash
        private const int Iterations = 10000; // Number of iterations

        /// <summary>
        /// Gets or sets the Id of the user
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the users email
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the users password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the creation date for the user
        /// </summary>
        public DateTime CreatedDate { get; private set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the updated date for the user
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Hashes the password string
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            // Hash the password and set the PasswordHash property
            PasswordHash = HashPassword(password);
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Hashes a password string
        /// </summary>
        /// <param name="password">The pasword to hash</param>
        /// <returns>The hashed string</returns>
        public static string HashPassword(string password)
        {
            // Generate a random salt
            var salt = GenerateSalt();

            // Convert password to bytes
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            // Use PBKDF2 to hash the password
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, Iterations))
            {
                var hashBytes = pbkdf2.GetBytes(HashSize);

                // Combine salt and hash
                var hashWithSaltBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashWithSaltBytes, 0, SaltSize);
                Array.Copy(hashBytes, 0, hashWithSaltBytes, SaltSize, HashSize);

                // Return base64-encoded string
                return Convert.ToBase64String(hashWithSaltBytes);
            }
        }

        /// <summary>
        /// Verifies the password against a hashed password
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <param name="storedHash"></param>
        /// <returns>bool indicating if the passwords match</returns>
        public static bool VerifyPassword(string password, string storedHash)
        {
            // Convert base64 string back to byte array
            var hashWithSaltBytes = Convert.FromBase64String(storedHash);

            // Extract salt from the stored hash
            var salt = new byte[SaltSize];
            Array.Copy(hashWithSaltBytes, 0, salt, 0, SaltSize);

            // Extract the hash from the stored hash
            var storedHashBytes = new byte[HashSize];
            Array.Copy(hashWithSaltBytes, SaltSize, storedHashBytes, 0, HashSize);

            // Hash the input password using the extracted salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                var hashBytes = pbkdf2.GetBytes(HashSize);

                // Compare the stored hash with the newly computed hash
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i] != storedHashBytes[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
