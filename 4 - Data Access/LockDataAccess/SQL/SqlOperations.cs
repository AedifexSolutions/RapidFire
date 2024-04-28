namespace LockDataAccess.SQL;

public class SqlOperations
{
    public static readonly string SelectAllLocks = "Select * from DistributedLocks";
    public static readonly string SelectALock = "Select * from DistributedLocks where [Key] = @Key";

    public static readonly string commandText = @" INSERT INTO DistributedLock 
                                                        (Key, MachineName, OwnerId, AcquiredAt, LockCount, CreatedTime, ModifiedTime)
                                                        VALUES 
                                                        (@key, @machineName, @ownerId, @acquiredAt, @lockCount, @createdTime, @modifiedTime)
                                                        WHERE NOT EXISTS (SELECT 1 FROM DistributedLock WHERE Key = @key)";
}
