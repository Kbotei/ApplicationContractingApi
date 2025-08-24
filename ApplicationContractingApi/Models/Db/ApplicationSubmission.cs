namespace ApplicationContractingApi.Models.Db;

public partial class ApplicationSubmission
{
    public Guid Id { get; set; }

    public Guid SubmissionId { get; set; }

    public string ApplicationType { get; set; } = null!;

    public Guid UserId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual ICollection<ApplicationFieldSubmission> ApplicationFieldSubmissions { get; set; } = new List<ApplicationFieldSubmission>();

    public virtual ICollection<ApplicationMobileSubmission> ApplicationMobileSubmissions { get; set; } = new List<ApplicationMobileSubmission>();

    public virtual User User { get; set; } = null!;
}
