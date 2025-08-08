namespace MobileAppApi.Models.Network;

public record ApplicationSubmissionRequest
{
    public Guid SubmissionId { get; set; }

    public List<OrderedField> Fields { get; set; } = [];
}
