namespace ILockDataAccess
{
    using DataModel;

    /// <summary>
    ///     Represents a database context for performing data access operations related to distributed locks.
    /// </summary>
    public interface IDataLockDbContext
    {
        /// <summary>
        ///     Retrieves a list of distributed locks from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of DistributedLock objects.</returns>
        Task<List<DistributedLock>> GetLocks();

        /// <summary>
        ///     Retrieves a distributed lock by its key from the data lock context.
        /// </summary>
        /// <param name="key">The key of the distributed lock.</param>
        /// <returns>
        ///     The distributed lock object with the specified key, or null if the lock does not exist.
        /// </returns>
        Task<DistributedLock> GetLock(string key);

        /// <summary>
        ///     Acquires a distributed lock for a specific job, machine, and owner.
        /// </summary>
        /// <param name="jobName">The name of the job.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="ownerId">The unique ID of the owner.</param>
        /// <returns>Returns true if the lock is successfully acquired; otherwise, returns false.</returns>
        Task<bool> AcquireLock(string jobName, string machineName, string ownerId);

        /// <summary>
        ///     Releases a distributed lock for a specific job, machine, and owner.
        /// </summary>
        /// <param name="jobName">The name of the job.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <param name="ownerId">The ID of the owner.</param>
        /// <returns>true if the lock was successfully released; otherwise, false.</returns>
        Task<bool> ReleaseLock(string jobName, string machineName, string ownerId);
    }
}
