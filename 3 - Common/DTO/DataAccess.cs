namespace DTO;

/// <summary>
///     Represents the data access configuration, including connection string, user ID, and password.
/// </summary>
public class DataAccess
{
    /// <summary>
    ///     Gets or sets the connection string for data access.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    ///     Gets or sets the user ID for data access.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     Gets or sets the password for data access.
    /// </summary>
    public string Password { get; set; }
}
