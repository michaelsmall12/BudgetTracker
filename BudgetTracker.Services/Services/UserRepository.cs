using BudgetTracker.Entites;
using BudgetTracker.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Cryptography;

namespace BudgetTracker.Services.Services
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Gets the logger for the repository
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Gets the database context for the repository
        /// </summary>
        private readonly BudgetTrackerDBContext dbContext;

        /// <summary>
        /// Constructor for the User Repository
        /// </summary>
        /// <param name="budgetTrackerDBContext">The database context to be used in the repository</param>
        /// <param name="logger">The logger to be used in the repository</param>
        public UserRepository(BudgetTrackerDBContext budgetTrackerDBContext, ILogger logger)
        {
            dbContext = budgetTrackerDBContext;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>bool indicating if the user was added successfully</returns>
        public async Task<bool> AddUser(User user)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                logger.Information($"Adding new user {user}");
                user.PasswordHash = HashPassword(user.PasswordHash);
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"Exception thrown in {nameof(AddUser)}", ex);
            }

            return false;
        }

        /// <summary>
        /// Updates a user in the database
        /// </summary>
        /// <param name="username">The usename to update</param>
        /// <param name="email">The email to check</param>
        /// <returns>bool indicating if the update was successful</returns>
        public async Task<bool> CheckUserExists(string username, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(username));
                logger.Information($"Checking user exists {username}");
                return await dbContext.Users.AnyAsync(x => x.UserName == username || x.UserEmail == email);
            }
            catch (Exception ex)
            {
                logger.Information($"Exception thrown in {nameof(CheckUserExists)}", ex);
            }

            return false;
        }

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="user">The user to delete from the database</param>
        /// <returns>bool indicating if the delete was successfull</returns>
        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                logger.Information($"Deleting user {user}");
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"Exception thrown in {nameof(DeleteUser)}", ex);
            }

            return false;
        }

        /// <summary>
        /// Checks if a user exists
        /// </summary>
        /// <param name="username">The username to check for</param>
        /// <param name="email">The email to check for</param>
        /// <returns>bool indicating if the account exists</returns>
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                if (user == null) throw new ArgumentNullException(nameof(user));
                logger.Information($"Updating user {user}");
                dbContext.Users.Update(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"Exception thrown in {nameof(UpdateUser)}", ex);
            }

            return false;
        }

        /// <summary>
        /// Attempts to log a user into the application
        /// </summary>
        /// <param name="userName">The username to try and log in</param>
        /// <param name="password">The password to try log in</param>
        /// <returns>bool indicating if the login was successfull</returns>
        public async Task<bool> Login(string userName, string password)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x=>x.UserName == userName);
                //var hashedPassword = HashPassword(password);
                return VerifyPassword(user, password);
                //return await dbContext.Users.AnyAsync(x => x.PasswordHash == hashedPassword && x.UserEmail == userName || x.UserName == userName);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception thrown in {nameof(Login)}", ex);
            }

            return false;
        }

        /// <summary>
        /// Verifies the password of the user trying to login
        /// </summary>
        /// <param name="user">The user trying to login</param>
        /// <param name="password">The password of the user trying to login</param>
        /// <returns>bool indicating if the passsword is correct</returns>
        public bool VerifyPassword(User user, string password)
        {
            return VerifyPasswordHash(password, user.PasswordHash);
        }

        /// <summary>
        /// Hashes a password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hashed password</returns>
        private string HashPassword(string password)
        {
            var salt = GenerateSalt();
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        /// <summary>
        /// Verifies  a password hash matches the existing hash
        /// </summary>
        /// <param name="password">The passsword to hash</param>
        /// <param name="storedHash">The stored hash to chech against</param>
        /// <returns>bool inidcating if the passwords match</returns>
        /// <exception cref="FormatException">Exception to be thrown if the hash doesnt match the expected format</exception>
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Unexpected hash format.");

            var salt = Convert.FromBase64String(parts[0]);
            var storedHashValue = parts[1];

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed == storedHashValue;
        }

        /// <summary>
        /// Generates the salt to be used in the password hashing
        /// </summary>
        /// <returns>The byte array for the salt</returns>
        private byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}