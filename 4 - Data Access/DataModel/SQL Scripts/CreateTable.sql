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

CREATE TABLE JobPriority
(
    Id   INT PRIMARY KEY,
    Name NVARCHAR(50)
);

INSERT INTO JobPriority (Id, Name)
VALUES (0, 'High'),
       (1, 'Medium'),
       (2, 'Low');

CREATE TABLE JobStatus
(
    Id   INT PRIMARY KEY,
    Name NVARCHAR(50)
);

INSERT INTO JobStatus (Id, Name)
VALUES (0, 'Scheduled'),
       (1, 'Processing'),
       (2, 'Completed'),
       (3, 'Failed'),
       (4, 'Paused'),
       (5, 'Retry');

CREATE TABLE JobType
(
    Id   INT PRIMARY KEY,
    Name NVARCHAR(50)
);

INSERT INTO JobType (Id, Name)
VALUES (0, 'Immediate'),
       (1, 'Delayed'),
       (2, 'Recurring');

CREATE TABLE Jobs
(
    Id            UNIQUEIDENTIFIER PRIMARY KEY,
    Name          NVARCHAR(MAX),
    [Group]       NVARCHAR(MAX),
    Description   NVARCHAR(MAX),
    MethodName    NVARCHAR(MAX),
    Code          NVARCHAR(MAX),
    JobArgs       NVARCHAR(MAX),
    ScheduledAt   DATETIME2,
    Status        INT FOREIGN KEY REFERENCES JobStatus (Id),
    JobType       INT FOREIGN KEY REFERENCES JobType (Id),
    RetryCount    INT,
    MaxRetryCount INT,
    RetryInterval TIME,
    ErrorDetails  NVARCHAR(MAX),
    Priority      INT FOREIGN KEY REFERENCES JobPriority (Id),
    InstanceId    NVARCHAR(MAX),
    CompletedAt   DATETIME2,
    CreatedAt     DATETIME2,
    CreatedBy     NVARCHAR(MAX)
)

CREATE TABLE JobFailHistory
(
    Id           UNIQUEIDENTIFIER PRIMARY KEY,
    JobId        UNIQUEIDENTIFIER,
    ErrorDetails NVARCHAR(MAX),
    FailAt       DATETIME2,
    FOREIGN KEY (JobId) REFERENCES Jobs (Id)
)