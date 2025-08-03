namespace MobileAppApi.NetworkModels
{
    public class RateCalculationRequest
    {
        public required Guid RequestId { get; set; }

        public decimal AmountToBeFinanced { get; set; }

        public DateOnly ApplicationDate { get; set; }

        public int NumberOfPayments { get; set; }

        public required string PromotionalOptions { get; set; }

        public DateOnly FirstPaymentDate { get; set; }
    }
}
