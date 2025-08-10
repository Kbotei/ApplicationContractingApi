using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class Client
{
    public Guid Id { get; set; }

    public string ClientNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string A2countryCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApplicationSubmission> ApplicationSubmissions { get; set; } = new List<ApplicationSubmission>();

    public virtual ICollection<ContractingSubmission> ContractingSubmissions { get; set; } = new List<ContractingSubmission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
