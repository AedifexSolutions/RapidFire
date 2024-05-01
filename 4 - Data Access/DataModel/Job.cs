namespace DataModel
{
    /// <summary>
    ///     Represents a job.
    /// </summary>
    public class Job
    {
        /// <summary>
        ///     Represents the unique identifier of a job.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Represents a job type name.
        /// </summary>
        public string Name { get; set; }

        /// Group
        /// Represents the group of a job.
        /// /
        public string Group { get; set; }

        /// <summary>
        ///     Represents a job in the system.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the method name associated with the job.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        ///     Represents the code to execute by the job.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Represents the job arguments for a job.
        /// </summary>
        public string JobArgs { get; set; }

        /// <summary>
        ///     Represents the scheduled date and time for a job.
        /// </summary>
        public DateTime ScheduledAt { get; set; }

        /// <summary>
        ///     Represents the status of a job.
        /// </summary>
        public JobStatus Status { get; set; }

        /// <summary>
        ///     Represents the type of job.
        /// </summary>
        public JobType JobType { get; set; } = JobType.Immediate;

        /// <summary>
        ///     Represents the number of times a job has been retried.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        ///     Represents the maximum number of retries for a job.
        /// </summary>
        /// <remarks>
        ///     The MaxRetryCount property specifies the maximum number of times a job can be retried in case of failure.
        /// </remarks>
        public int MaxRetryCount { get; set; }

        /// <summary>
        ///     Represents the time interval between retry attempts for a job.
        /// </summary>
        /// <value>
        ///     The retry interval for the job.
        /// </value>
        public TimeSpan RetryInterval { get; set; }

        /// <summary>
        ///     Contains the details of an error occurred during the execution of a job.
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        ///     Represents the priority of a job.
        /// </summary>
        public JobPriority Priority { get; set; } = JobPriority.Medium;

        /// <summary>
        ///     Gets or sets the instance ID of the job.
        /// </summary>
        /// <remarks>
        ///     An instance ID is a unique identifier assigned to each instance of the job agent.
        /// </remarks>
        public string InstanceId { get; set; }

        /// <summary>
        ///     Gets or sets the completion date and time of the job.
        /// </summary>
        public DateTime? CompletedAt { get; set; }

        /// <summary>
        ///     Gets or sets the date and time the job was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Gets or sets the user who created the job.
        /// </summary>
        /// <value>The user who created the job.</value>
        public string CreatedBy { get; set; }
    }
}
