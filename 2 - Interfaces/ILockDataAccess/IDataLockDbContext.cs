namespace ILockDataAccess;

using DataModel;

public interface IDataLockDbContext
{
    Task<List<DistributedLock>> GetLocks();

    Task<DistributedLock> GetLock(string key);
}
