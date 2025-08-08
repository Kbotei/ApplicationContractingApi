namespace MobileAppApi.Models.Network;

public record CalculationResponse
{
    public decimal AmountFinanced { get; set; }

    public decimal AdditionalFees { get; set; }

    public int NumberOfPayments { get; set; }

    public required string PromotionalOptions { get; set; }

    public DateOnly FirstPaymentDate { get; set; }

    public DateOnly CancelationDate { get; set; }
}
