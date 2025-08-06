using Microsoft.AspNetCore.Mvc;
using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;
using MobileAppApi.Stores;

namespace MobileAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditApplicationController(ILogger<CreditApplicationController> logger, MobileApiContext mobileApiContext, AccountStore accountStore) : ControllerBase
    {
        private readonly ILogger<CreditApplicationController> _logger = logger;
        private readonly MobileApiContext _mobileApiContext = mobileApiContext;
        private readonly AccountStore _accountStore = accountStore;

        [HttpGet(Name = "check-availability")]
        public CreditApplicationAvailabilityResponse CheckAvailability()
        {
            // Query backend API or database to determine the credit applications availability based on staffing or infrastructure status.
            // Some applications may need to be manually underwritten. In those cases we would want to warn the client if staff are not
            // available to underwrite the application. The client may still wish to submit an application in hopes that it can be automatically underwritten.
            return new CreditApplicationAvailabilityResponse { Available = true, StaffAvailable = true };
        }

        [HttpPost(Name = "submit-application")]
        public async Task<ActionResult> Submit(CreditApplicationSubmissionRequest request)
        {
            var success = await _accountStore.SaveCreditApplicationData(request);

            // TODO: add success message to response
            return success ? Ok() : BadRequest();
        }
    }
}
