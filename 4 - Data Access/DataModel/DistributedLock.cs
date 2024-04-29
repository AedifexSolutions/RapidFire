namespace DataModel
{
    public class DistributedLock
    {
        public string Key { get; set; }

        public string MachineName { get; set; }

        public string OwnerId { get; set; }

        public DateTime AcquiredAt { get; set; }

        public DateTime? LockExpiryTime { get; set; }

        public int LockCount { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}