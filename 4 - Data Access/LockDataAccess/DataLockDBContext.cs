namespace LockDataAccess;

using DataModel;
using ILockDataAccess;
using Microsoft.EntityFrameworkCore;

public class DataLockDbContext : DbContext, IDataLockDbContext
{
    public DataLockDbContext(DbContextOptions<DataLockDbContext> options) : base(options)
    {
    }

    public DbSet<DistributedLock> Locks { get; set; }

    public IQueryable<DistributedLock> DistributedLocks => Locks.AsQueryable();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DistributedLock>(entity =>
            {
                entity.HasKey(e => e.Key);
                entity.Property(e => e.Key).HasMaxLength(256).IsRequired();
                entity.Property(e => e.MachineName).HasMaxLength(256);
                entity.Property(e => e.OwnerId);
                entity.Property(e => e.AcquiredAt);
                entity.Property(e => e.LockExpiryTime);
                entity.Property(e => e.LockCount);
                entity.Property(e => e.CreatedTime);
                entity.Property(e => e.ModifiedTime);
                entity.ToTable("DistributedLocks");
            });
    }
}
