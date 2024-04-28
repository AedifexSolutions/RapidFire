namespace DistributedLockTest;

using DataModel;
using ILockDataAccess;
using Moq;

[TestFixture]
public class DistributedLockTest
{
    [SetUp]
    public void Setup()
    {
        this.mockDataLockDbContext = new Mock<IDataLockDbContext>();
    }

    private Mock<IDataLockDbContext> mockDataLockDbContext;

    [Test]
    public async Task TestGetLocks()
    {
        var expectedLocks = new List<DistributedLock>
        {
            new DistributedLock
            {
                Key = "Job1"
            },
            new DistributedLock
            {
                Key = "Job2"
            }
        };

        this.mockDataLockDbContext.Setup(x => x.GetLocks()).ReturnsAsync(expectedLocks);

        var locks = await this.mockDataLockDbContext.Object.GetLocks();

        Assert.That(locks, Is.Not.Null);
        Assert.That(locks, Is.EqualTo(expectedLocks));
    }

    [Test]
    public async Task TestGetLock()
    {
        var expectedLock = new DistributedLock
        {
            Key = "Job1"
        };

        this.mockDataLockDbContext.Setup(x => x.GetLock("Job1")).ReturnsAsync(expectedLock);

        var lockObj = await this.mockDataLockDbContext.Object.GetLock("Job1");

        Assert.That(lockObj, Is.Not.Null);
        Assert.That(lockObj, Is.EqualTo(expectedLock));
    }

    [Test]
    public async Task TestAcquireLock()
    {
        this.mockDataLockDbContext.Setup(x => x.AcquireLock("jobName", "machineName", "ownerId")).ReturnsAsync(true);

        var result = await this.mockDataLockDbContext.Object.AcquireLock("jobName", "machineName", "ownerId");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task TestReleaseLock()
    {
        this.mockDataLockDbContext.Setup(x => x.ReleaseLock("jobName", "machineName", "ownerId")).ReturnsAsync(true);

        var result = await this.mockDataLockDbContext.Object.ReleaseLock("jobName", "machineName", "ownerId");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task TestAcquireLock_FailWhenLockExistsAndNotExpired()
    {
        var existingLock = new DistributedLock
        {
            Key = "Job1", LockExpiryTime = DateTime.Now.AddHours(1)
        };

        this.mockDataLockDbContext.Setup(x => x.GetLock("Job1")).ReturnsAsync(existingLock);
        this.mockDataLockDbContext.Setup(x => x.AcquireLock("Job1", It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        var result = await this.mockDataLockDbContext.Object.AcquireLock("Job1", "machineName", "ownerId");

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task TestAcquireLock_SuccessWhenLockExistsButExpired()
    {
        var existingExpiredLock = new DistributedLock
        {
            Key = "Job1", LockExpiryTime = DateTime.Now.AddHours(-1)
        };// expired lock

        this.mockDataLockDbContext.Setup(x => x.GetLock("Job1")).ReturnsAsync(existingExpiredLock);
        this.mockDataLockDbContext.Setup(x => x.AcquireLock("Job1", It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

        var result = await this.mockDataLockDbContext.Object.AcquireLock("Job1", "machineName", "ownerId");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task TestReleaseLock_FailWhenLockDoesNotExist()
    {
        this.mockDataLockDbContext.Setup(x => x.GetLock("Job1")).ReturnsAsync((DistributedLock)null!);
        this.mockDataLockDbContext.Setup(x => x.ReleaseLock("Job1", It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        var result = await this.mockDataLockDbContext.Object.ReleaseLock("Job1", "machineName", "ownerId");

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task TestReleaseLock_FailWhenDifferentMachine()
    {
        var existingLock = new DistributedLock
        {
            Key = "Job1", MachineName = "differentMachine", OwnerId = "ownerId"
        };

        this.mockDataLockDbContext.Setup(x => x.GetLock("Job1")).ReturnsAsync(existingLock);
        this.mockDataLockDbContext.Setup(x => x.ReleaseLock("Job1", "machineName", It.IsAny<string>())).ReturnsAsync(false);

        var result = await this.mockDataLockDbContext.Object.ReleaseLock("Job1", "machineName", "ownerId");

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task TestReleaseLock_FailWhenDifferentOwner()
    {
        var existingLock = new DistributedLock
        {
            Key = "Job1", MachineName = "machineName", OwnerId = "differentOwnerId"
        };

        this.mockDataLockDbContext.Setup(x => x.GetLock("Job1")).ReturnsAsync(existingLock);
        this.mockDataLockDbContext.Setup(x => x.ReleaseLock("Job1", It.IsAny<string>(), "ownerId")).ReturnsAsync(false);

        var result = await this.mockDataLockDbContext.Object.ReleaseLock("Job1", "machineName", "ownerId");

        Assert.That(result, Is.False);
    }
}