namespace MobileAppApi.Models.Network
{
    public record CreditApplicationAvailabilityResponse
    {
        public bool Available { get; set; }

        public bool StaffAvailable { get; set; }
    }
}
