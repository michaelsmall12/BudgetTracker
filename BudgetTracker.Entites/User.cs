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
        /// Gets or sets the income stream for the user
        /// </summary>
        public List<IncomeStream> IncomeStreams { get; set; }

        /// <summary>
        /// Gets or sets the outgoing streams
        /// </summary>
        public List<OutGoings> Outgoings { get; set; }
        
    }
}
