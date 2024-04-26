namespace LockDataAccess;

using DataModel;
using Microsoft.EntityFrameworkCore;

public class DataLockDbContext : DbContext
{
    public DataLockDbContext(DbContextOptions<DataLockDbContext> options) : base(options)
    {
    }

    private DbSet<DistributedLock> Locks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
