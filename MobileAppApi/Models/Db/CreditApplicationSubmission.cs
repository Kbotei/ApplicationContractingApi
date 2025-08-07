namespace MobileAppApi.Models.Db;

public partial class CreditApplicationSubmission
{
    public Guid Id { get; set; }

    public Guid SubmissionId { get; set; }

    public Guid ClientId { get; set; }

    public Guid DeviceId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<CreditApplicationFieldSubmission> CreditApplicationFieldSubmissions { get; set; } = new List<CreditApplicationFieldSubmission>();

    public virtual Device Device { get; set; } = null!;
}
