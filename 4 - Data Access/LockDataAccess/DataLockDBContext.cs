namespace LockDataAccess;

using Dapper;
using DataModel;
using DTO;
using ILockDataAccess;
using Microsoft.Data.SqlClient;
using SQL;

public class DataLockDbContext : IDataLockDbContext
{
    private readonly string connectionString;

    public DataLockDbContext(DbConnectionSettings dbconnectionSettings)
    {
        this.connectionString =
            $"{dbconnectionSettings.ConnectionString};User Id={dbconnectionSettings.UserId};Password={dbconnectionSettings.Password}";
    }

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
}