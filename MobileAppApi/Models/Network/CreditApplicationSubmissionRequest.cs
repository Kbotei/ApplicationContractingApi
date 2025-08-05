namespace MobileAppApi.Models.Network
{
    public record CreditApplicationSubmissionRequest
    {
        public Guid SubmissionId { get; set; }

        public List<OrderedField> Fields { get; set; } = [];
    }
}
