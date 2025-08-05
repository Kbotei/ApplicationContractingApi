using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class Client
{
    public Guid Id { get; set; }

    public int ClientNumber { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ContractingSubmission> ContractingSubmissions { get; set; } = new List<ContractingSubmission>();

    public virtual ICollection<CreditApplicationSubmission> CreditApplicationSubmissions { get; set; } = new List<CreditApplicationSubmission>();
}
