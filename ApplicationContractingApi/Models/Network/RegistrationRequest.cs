namespace MobileAppApi.Models.Network;

public class RegistrationRequest
{
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public required string ClientNumber { get; set; }

    public required string DeviceModel { get; set; }

    public required string OperatingSystem { get; set; }

    public required string OperatingSystemVersion { get; set; }
}
