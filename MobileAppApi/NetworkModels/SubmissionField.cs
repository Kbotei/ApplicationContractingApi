namespace MobileAppApi.NetworkModels
{
    public class SubmissionField
    {
        public required string FieldNamespace { get; set; }

        public required string FieldName { get; set; }

        public required string FieldType { get; set; }

        public string? FieldValue { get; set; }
    }
}
