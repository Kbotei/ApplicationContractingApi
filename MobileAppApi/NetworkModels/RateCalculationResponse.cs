namespace MobileAppApi.NetworkModels
{
    public class RateCalculationResponse
    {
        public decimal AmountFinanced { get; set; }

        public decimal AdditionalFees { get; set; }

        public int NumberOfPayments { get; set; }

        public required string PromotionalOptions { get; set; }

        public DateOnly FirstPaymentDate { get; set; }
    }
}
