using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class Device
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OperatingSystem { get; set; } = null!;

    public string OperatingSystemVersion { get; set; } = null!;

    public DateTime LastActiveAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApplicationSubmission> ApplicationSubmissions { get; set; } = new List<ApplicationSubmission>();

    public virtual ICollection<ContractingSubmission> ContractingSubmissions { get; set; } = new List<ContractingSubmission>();

    public virtual User User { get; set; } = null!;
}
