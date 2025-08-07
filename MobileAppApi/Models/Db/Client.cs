using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class Client
{
    public Guid Id { get; set; }

    public int ClientNumber { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApplicationSubmission> ApplicationSubmissions { get; set; } = new List<ApplicationSubmission>();

    public virtual ICollection<ContractingSubmission> ContractingSubmissions { get; set; } = new List<ContractingSubmission>();
}
