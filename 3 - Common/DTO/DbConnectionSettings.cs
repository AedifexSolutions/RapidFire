namespace DTO
{
    /// <summary>
    /// Represents the data access configuration, including connection string, user ID, and password.
    /// </summary>
    public class DbConnectionSettings
    {
        /// <summary>
        /// Gets or sets the connection string for data access.
        /// </summary>
        public required string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the user ID for data access.
        /// </summary>
        public required string UserId { get; set; }

        /// <summary>
        /// Gets or sets the password for data access.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the minimum lock timeout period in minutes.
        /// </summary>
        public int LockTimeoutMin { get; set; }
    }
}
