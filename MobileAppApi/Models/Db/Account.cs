using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class Account
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? CurrentDecision { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }
}
