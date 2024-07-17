namespace DataModel
{
    /// <summary>
    ///     Represents the history of job failures.
    /// </summary>
    public class JobFailHistory
    {
        /// <summary>
        ///     Gets or sets the Id of the job fail history.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Represents the unique identifier for a job.
        /// </summary>
        /// <remarks>
        ///     The JobId property is used to uniquely identify a job within the system.
        ///     It is a Guid value that is automatically assigned when a new job is created.
        /// </remarks>
        public Guid JobId { get; set; }

        /// <summary>
        ///     Represents the error details associated with a failed job.
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        ///     Gets or sets the time at which the job failed.
        /// </summary>
        public DateTime FailAt { get; set; }
    }
}
