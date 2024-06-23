namespace LockDataAccess
{
    using Dapper;
    using DataModel;
    using DTO;
    using ILockDataAccess;
    using Microsoft.Data.SqlClient;
    using SQL;

    /// <summary>
    ///     Represents a database context for performing data access operations related to distributed locks.
    /// </summary>
    public class DataLockDbContext(DbConnectionSettings dbConnectionSettings) : IDataLockDbContext
    {
        /// <summary>
        ///     Represents a connection string for connecting to a database.
        /// </summary>
        private readonly string connectionString =
            $"{dbConnectionSettings.ConnectionString};User Id={dbConnectionSettings.UserId};Password={dbConnectionSettings.Password}";

        /// <summary>
        ///     Represents the minimum time, in minutes, for acquiring and releasing a distributed lock.
        /// </summary>
        private readonly int LockTimeoutMin = dbConnectionSettings.LockTimeoutMin;

        /// <summary>
        ///     Retrieves a list of all locks from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of DistributedLock objects.</returns>
        public async Task<List<DistributedLock>> GetLocks()
        {
            await using var connection = new SqlConnection(this.connectionString);
            var locks = (await connection.QueryAsync<DistributedLock>(SqlOperations.SelectAllLocks)).ToList();
            return locks;
        }

        /// <summary>
        ///     Retrieves a distributed lock from the database based on the specified key.
        /// </summary>
        /// <param name="key">The key used to identify the lock.</param>
        /// <returns>The retrieved <see cref="DistributedLock" /> object, or null if no lock is found with the specified key.</returns>
        public async Task<DistributedLock> GetLock(string key)
        {
            await using var connection = new SqlConnection(this.connectionString);
            var parameters = new
            {
                Key = key
            };

            var lck = await connection.QueryFirstOrDefaultAsync<DistributedLock>(SqlOperations.SelectALock, parameters);
            return lck;
        }

        /// <summary>
        ///     Acquires a lock for a specified job.
        /// </summary>
        /// <param name="jobName">The name of the job to acquire the lock for.</param>
        /// <param name="machineName">The name of the machine where the job is being executed.</param>
        /// <param name="ownerId">The unique identifier of the owner of the lock.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation. The task result contains a boolean value indicating whether the lock was successfully acquired.</returns>
        public async Task<bool> AcquireLock(string jobName, string machineName, string ownerId)
        {
            await using var connection = new SqlConnection(this.connectionString);

            var parameters = new
            {
                key = jobName,
                machineName,
                ownerId,
                acquiredAt = DateTime.UtcNow,
                lockCount = 1,
                createdTime = DateTime.UtcNow,
                modifiedTime = DateTime.UtcNow,
                lockExpiryTime = DateTime.UtcNow.AddMinutes(this.LockTimeoutMin)
            };

            var cnt = await connection.ExecuteAsync(SqlOperations.AcquireLock, parameters);
            return cnt > 0;
        }

        /// <summary>
        ///     Releases a distributed lock for a specific job, machine, and owner.
        /// </summary>
        /// <param name="jobName">The name of the job.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="ownerId">The ID of the owner.</param>
        /// <returns>true if the lock was successfully released; otherwise, false.</returns>
        public async Task<bool> ReleaseLock(string jobName, string machineName, string ownerId)
        {
            await using var connection = new SqlConnection(this.connectionString);

            var parameters = new
            {
                key = jobName, machineName, ownerId
            };

            var cnt = await connection.ExecuteAsync(SqlOperations.ReleaseLock, parameters);
            return cnt > 0;
        }
    }
}
