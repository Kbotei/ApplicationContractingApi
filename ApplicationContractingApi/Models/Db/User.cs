namespace ApplicationContractingApi.Models.Db;

public partial class User
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApplicationSubmission> ApplicationSubmissions { get; set; } = new List<ApplicationSubmission>();

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
