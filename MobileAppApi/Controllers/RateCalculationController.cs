using Microsoft.AspNetCore.Mvc;
using MobileAppApi.NetworkModels;

namespace MobileAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateCalculationController(ILogger<RateCalculationController> logger) : ControllerBase
    {
        private readonly ILogger<RateCalculationController> _logger = logger;

        [HttpPost(Name = "calculate")]
        public RateCalculationResponse Calculate(RateCalculationRequest calculationRequest) 
        {
            /*
            This is where we would call a specialized backend service or library to calculate the rate. The backend service would 
            handle the various legal requirements for the supported countries and local regions. This could include items like
            cancel dates or specific fees required (e.g. Florida doc stamp)
            */ 

            // For now return the original request + random values for additional properties.
            var random = new Random();
            var additionalFees = new decimal(random.NextDouble() * 100);

            return new RateCalculationResponse {
                AdditionalFees = additionalFees,
                AmountFinanced = calculationRequest.AmountToBeFinanced + additionalFees,
                FirstPaymentDate = calculationRequest.FirstPaymentDate,
                NumberOfPayments = calculationRequest.NumberOfPayments,
                PromotionalOptions = calculationRequest.PromotionalOptions,
            };

        }
    }
}
