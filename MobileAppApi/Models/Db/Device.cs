using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class Device
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public string OperatingSystem { get; set; } = null!;

    public string OperatingSystemVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CreditApplicationSubmission> CreditApplicationSubmissions { get; set; } = new List<CreditApplicationSubmission>();
}
