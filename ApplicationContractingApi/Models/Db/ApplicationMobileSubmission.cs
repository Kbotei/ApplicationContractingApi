namespace ApplicationContractingApi.Models.Db;

public partial class ApplicationMobileSubmission
{
    public Guid Id { get; set; }

    public Guid ApplicationSubmissionId { get; set; }

    public Guid DeviceId { get; set; }

    public virtual ApplicationSubmission ApplicationSubmission { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;
}
