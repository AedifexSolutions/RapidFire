namespace DataModel
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public string MethodName { get; set; }
        public string JobArgs { get; set; }
        public DateTime ScheduledAt { get; set; }
        public JobStatus Status { get; set; }
        public JobType JobType { get; set; }
        public int RetryCount { get; set; }
        public int MaxRetryCount { get; set; }
        public TimeSpan RetryInterval { get; set; }
        public string ErrorDetails { get; set; }
        public JobPriority Priority { get; set; }
        public string InstanceId { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
