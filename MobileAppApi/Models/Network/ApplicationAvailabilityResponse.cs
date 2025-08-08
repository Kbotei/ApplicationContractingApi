namespace MobileAppApi.Models.Network;

public record ApplicationAvailabilityResponse
{
    public bool Available { get; set; }

    public bool StaffAvailable { get; set; }
}
