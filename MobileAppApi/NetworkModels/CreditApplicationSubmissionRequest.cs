namespace MobileAppApi.NetworkModels
{
    public class CreditApplicationSubmissionRequest
    {
        public Guid SubmissionId { get; set; }

        public List<SubmissionField> Fields { get; set; } = [];
    }
}
