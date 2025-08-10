using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class AccountField
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public string Namespace { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Account Account { get; set; } = null!;
}
