using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileAppApi.Models.Db;
using MobileAppApi.NetworkModels;

namespace MobileAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditApplicationController(ILogger<CreditApplicationController> logger, MobileApiContext mobileApiContext) : ControllerBase
    {
        private readonly ILogger<CreditApplicationController> _logger = logger;
        private readonly MobileApiContext _mobileApiContext = mobileApiContext;

        [HttpGet]
        public CreditApplicationAvailabilityResponse CheckAvailability()
        {
            // Query backend API or database to determine the credit applications availability based on staffing or infrastructure status.
            // Some applications may need to be manually underwritten. In those cases we would want to warn the client if staff are not
            // available to underwrite the application. The client may still wish to submit an application in hopes that it can be automatically underwritten.
            return new CreditApplicationAvailabilityResponse { Available = true, StaffAvailable = true };
        }

        [HttpPost(Name = "SubmitApplication")]
        public async Task<ActionResult> Submit(CreditApplicationSubmissionRequest request)
        {
            using (var context = _mobileApiContext)
            {
                // Submissions should be idempotent. A duplicate submission could occur due to network conditions preventing the
                // API consumer from receiving the APIs response.
                if (context.CreditApplicationSubmissions.Any(c => c.SubmissionId == request.SubmissionId))
                {
                    return Ok();
                }

                var fields = request.Fields.ConvertAll(f => new CreditApplicationFieldSubmission
                {
                    FieldNamespace = f.FieldNamespace,
                    FieldName = f.FieldName,
                    FieldType = f.FieldType,
                    FieldValue = f.FieldValue,
                });

                // TODO: replace parsed ids with lookup, then eventually tap into identity system?.
                var newSubmission = new CreditApplicationSubmission
                {
                    SubmissionId = request.SubmissionId,
                    ClientId = Guid.Parse("d33f9df0-e8c1-47bd-bf1a-25713680cce2"),
                    DeviceId = Guid.Parse("aabddf66-7c41-4a6d-94d7-a9861249dcd5"),
                    CreditApplicationFieldSubmissions = fields,
                };

                context.CreditApplicationSubmissions.Add(newSubmission);
                await context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
