namespace ILockDataAccess;

using DataModel;

public interface IDataLockDbContext
{
    IQueryable<DistributedLock> DistributedLocks { get; }
}
