namespace ApplicationContractingApi.Models.Db;

public partial class ContractingSubmission
{
    public Guid Id { get; set; }

    public Guid SubmissionId { get; set; }

    public Guid ClientId { get; set; }

    public Guid DeviceId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<ContractingFieldSubmission> ContractingFieldSubmissions { get; set; } = new List<ContractingFieldSubmission>();

    public virtual Device Device { get; set; } = null!;
}
