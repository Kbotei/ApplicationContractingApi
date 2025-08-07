using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class ApplicationFieldSubmission
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public string FieldNamespace { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public string? FieldValue { get; set; }

    public string? LabelText { get; set; }

    public string? SelectedItemText { get; set; }

    public int ViewOrder { get; set; }

    public virtual ApplicationSubmission Application { get; set; } = null!;
}
