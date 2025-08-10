using System;
using System.Collections.Generic;

namespace MobileAppApi.Models.Db;

public partial class ApplicationSubmission
{
    public Guid Id { get; set; }

    public Guid SubmissionId { get; set; }

    public Guid ClientId { get; set; }

    public Guid DeviceId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public virtual ICollection<ApplicationFieldSubmission> ApplicationFieldSubmissions { get; set; } = new List<ApplicationFieldSubmission>();

    public virtual Client Client { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;
}
