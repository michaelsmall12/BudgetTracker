using BudgetTracker.Entites;

namespace BudgetTracker.Services.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>bool indicating if the user was added successfully</returns>
        Task<bool> AddUser(User user);

        /// <summary>
        /// Updates a user in the database
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <returns>bool indicating if the update was successful</returns>
        Task<bool> UpdateUser(User user);

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="user">The user to delete from the database</param>
        /// <returns>bool indicating if the delete was successfull</returns>
        Task<bool> DeleteUser(User user);

        /// <summary>
        /// Checks if a user exists
        /// </summary>
        /// <param name="username">The username to check for</param>
        /// <returns>bool indicating if the account exists</returns>
        Task<bool> CheckUserExists(string username);

        /// <summary>
        /// Attempts to log a user into the application
        /// </summary>
        /// <param name="userName">The username to try and log in</param>
        /// <param name="password">The password to try log in</param>
        /// <returns>bool indicating if the login was successfull</returns>
        Task<bool> Login(string userName, string password);
    }
}
