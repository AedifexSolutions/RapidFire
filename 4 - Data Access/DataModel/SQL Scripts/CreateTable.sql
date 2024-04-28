CREATE TABLE DistributedLocks
(
    [Key]          NVARCHAR(256) PRIMARY KEY,
    MachineName    NVARCHAR(256),
    OwnerId        NVARCHAR(MAX),
    AcquiredAt     DATETIME NOT NULL,
    LockExpiryTime DATETIME,
    LockCount      INT      NOT NULL,
    CreatedTime    DATETIME NOT NULL,
    ModifiedTime   DATETIME NOT NULL
)