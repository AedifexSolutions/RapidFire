namespace ILockDataAccess;

using DataModel;

public interface IDataLockDbContext
{
    Task<List<DistributedLock>> GetLocks();

    Task<DistributedLock> GetLock(string key);

    Task<bool> AcquireLock(string jobName, string machineName, string ownerId);

    Task<bool> ReleaseLock(string jobName, string machineName, string ownerId);
}