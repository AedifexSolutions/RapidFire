namespace DataModel;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("DistributedLocks")]
public class DistributedLock
{
    [Key] [Required] [StringLength(256)] public string Key { get; set; }

    [StringLength(256)] public string MachineName { get; set; }

    public string OwnerId { get; set; }

    public DateTime AcquiredAt { get; set; }

    public DateTime? LockExpiryTime { get; set; }

    public int LockCount { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime ModifiedTime { get; set; }
}
