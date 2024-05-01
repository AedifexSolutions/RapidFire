namespace LockDataAccess
{
    using Dapper;
    using DataModel;
    using DTO;
    using ILockDataAccess;
    using Microsoft.Data.SqlClient;
    using SQL;

    public class DataLockDbContext(DbConnectionSettings dbConnectionSettings) : IDataLockDbContext
    {
        private readonly string connectionString =
            $"{dbConnectionSettings.ConnectionString};User Id={dbConnectionSettings.UserId};Password={dbConnectionSettings.Password}";

        private readonly int LockTimeoutMin = dbConnectionSettings.LockTimeoutMin;

        public async Task<List<DistributedLock>> GetLocks()
        {
            await using var connection = new SqlConnection(this.connectionString);
            var locks = (await connection.QueryAsync<DistributedLock>(SqlOperations.SelectAllLocks)).ToList();
            return locks;
        }

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
