namespace ApplicationContractingApi.Models.Db;

public partial class ContractingFieldSubmission
{
    public Guid Id { get; set; }

    public Guid ContractingId { get; set; }

    public string FieldNamespace { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string? FieldValue { get; set; }

    public virtual ContractingSubmission Contracting { get; set; } = null!;
}
