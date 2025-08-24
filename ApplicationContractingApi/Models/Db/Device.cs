namespace ApplicationContractingApi.Models.Db;

public partial class Device
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OperatingSystem { get; set; } = null!;

    public string OperatingSystemVersion { get; set; } = null!;

    public DateTime LastActiveAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApplicationMobileSubmission> ApplicationMobileSubmissions { get; set; } = new List<ApplicationMobileSubmission>();

    public virtual ICollection<ContractingSubmission> ContractingSubmissions { get; set; } = new List<ContractingSubmission>();

    public virtual User User { get; set; } = null!;
}
