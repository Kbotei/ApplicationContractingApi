using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class CreditApplicationFieldSubmission
{
    public Guid Id { get; set; }

    public Guid CreditApplicationId { get; set; }

    public string FieldNamespace { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string? FieldValue { get; set; }

    public string? LabelText { get; set; }

    public string? SelectedItemText { get; set; }

    public int ViewOrder { get; set; }

    public virtual CreditApplicationSubmission CreditApplication { get; set; } = null!;
}
