using BudgetTracker.Entites;
using BudgetTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Services.Services
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Gets the logger for the repository
        /// </summary>
        private readonly ILogger<UserRepository> logger;

        /// <summary>
        /// Gets the database context for the repository
        /// </summary>
        private readonly BudgetTrackerDBContext dbContext;

        /// <summary>
        /// Constructor for the User Repository
        /// </summary>
        /// <param name="budgetTrackerDBContext">The database context to be used in the repository</param>
        /// <param name="logger">The logger to be used in the repository</param>
        public UserRepository(BudgetTrackerDBContext budgetTrackerDBContext, ILogger<UserRepository> logger) 
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
                logger.LogInformation($"Adding new user {user}");
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception thrown in {nameof(AddUser)}",ex);
            }

            return false;
        }

        /// <summary>
        /// Updates a user in the database
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <returns>bool indicating if the update was successful</returns>
        public async Task<bool> CheckUserExists(string username, string email)
        {
            try
            {
                if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(username));
                logger.LogInformation($"Checking user exists {username + email}");
                return await dbContext.Users.AnyAsync(x=>x.UserEmail == email && x.UserName == username);
            }
            catch(Exception ex)
            {
                logger.LogError($"Exception thrown in {nameof(CheckUserExists)}", ex);
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
                logger.LogInformation($"Deleting user {user}");
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception thrown in {nameof(DeleteUser)}", ex);
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
                logger.LogInformation($"Updating user {user}");
                dbContext.Users.Update(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception thrown in {nameof(UpdateUser)}", ex);
            }

            return false;
        }
    }
}
