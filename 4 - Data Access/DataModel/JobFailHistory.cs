namespace DataModel;

public class JobFailHistory
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public string ErrorDetails { get; set; }
    public DateTime FailAt { get; set; }
}
