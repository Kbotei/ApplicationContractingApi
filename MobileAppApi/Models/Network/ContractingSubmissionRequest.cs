namespace MobileAppApi.Models.Network;

public record ContractingSubmissionRequest
{
    public Guid SubmissionId { get; set; }

    public List<SimpleField> Fields { get; set; } = [];
}
