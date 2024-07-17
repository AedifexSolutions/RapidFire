namespace DataModel
{
    /// <summary>
    ///     Represents a distributed lock object.
    /// </summary>
    public class DistributedLock
    {
        /// <summary>
        ///     Represents the key property of the DistributedLock class.
        /// </summary>
        /// <value>
        ///     The key that uniquely identifies a distributed lock.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets the name of the machine associated with the distributed lock.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the ID of the owner of the distributed lock.
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        ///     Gets or sets the date and time when the lock was acquired.
        /// </summary>
        /// <value>
        ///     The date and time when the lock was acquired.
        /// </value>
        public DateTime AcquiredAt { get; set; }

        /// <summary>
        ///     Represents the expiry time of a distributed lock.
        /// </summary>
        /// <remarks>
        ///     The LockExpiryTime property of the DistributedLock class represents the time at which the lock will expire.
        ///     If the lock is not acquired before the lock expiry time, it can be acquired by another process or owner.
        /// </remarks>
        public DateTime? LockExpiryTime { get; set; }

        /// <summary>
        ///     Gets or sets the number of times the lock has been acquired.
        /// </summary>
        /// <remarks>
        ///     The LockCount property provides information about the number of times the lock has been acquired.
        ///     It is incremented each time the lock is acquired and decremented each time the lock is released.
        ///     The LockCount property can be used to track the usage of the lock and determine if it is currently in use or available.
        /// </remarks>
        public int LockCount { get; set; }

        /// <summary>
        ///     Gets or sets the time when the DistributedLock was created.
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        ///     Gets or sets the modified time of the distributed lock.
        /// </summary>
        /// <value>
        ///     The modified time represents the last time the distributed lock was modified.
        /// </value>
        public DateTime ModifiedTime { get; set; }
    }
}
