namespace LockDataAccess.SQL
{
    public class SqlOperations
    {
        public static readonly string SelectAllLocks = "Select * from DistributedLocks";

        public static readonly string SelectALock = "Select * from DistributedLocks where [Key] = @Key";

        public static readonly string AcquireLock =
            @"IF EXISTS (SELECT 1 FROM DistributedLocks WHERE [Key] = @key AND LockExpiryTime <= GETUTCDATE())
                                                        UPDATE DistributedLocks
                                                        SET MachineName = @machineName, OwnerId = @ownerId, AcquiredAt = @acquiredAt, LockExpiryTime = @lockExpiryTime, LockCount = @lockCount, ModifiedTime = @modifiedTime
                                                        WHERE [Key] = @key
                                                    ELSE IF NOT EXISTS (SELECT 1 FROM DistributedLocks WHERE [Key] = @key AND LockExpiryTime > GETDATE())
                                                        INSERT INTO DistributedLocks 
                                                            ([Key], MachineName, OwnerId, AcquiredAt, LockExpiryTime, LockCount, CreatedTime, ModifiedTime)
                                                        VALUES 
                                                            (@key, @machineName, @ownerId, @acquiredAt, @lockExpiryTime, @lockCount, @createdTime, @modifiedTime)
                                                    ";

        public static readonly string ReleaseLock =
            @"UPDATE DistributedLocks SET LockExpiryTime = '1753-01-01' WHERE [Key] = @key AND MachineName = @machineName AND OwnerId = @ownerId;";
    }
}