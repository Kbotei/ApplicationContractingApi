using Microsoft.AspNetCore.Mvc;
using MobileAppApi.Models.Network;
using MobileAppApi.Stores;

namespace MobileAppApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicationController(ILogger<ApplicationController> logger, ApplicationStore applicationStore) : ControllerBase
{
    private readonly ILogger<ApplicationController> _logger = logger;
    private readonly ApplicationStore _applicationStore = applicationStore;

    [HttpGet(Name = "check-availability")]
    public ApplicationAvailabilityResponse CheckAvailability()
    {
        // Query backend API or database to determine the credit applications availability based on staffing or infrastructure status.
        // Some applications may need to be manually underwritten. In those cases we would want to warn the client if staff are not
        // available to underwrite the application. The client may still wish to submit an application in hopes that it can be automatically underwritten.
        return new ApplicationAvailabilityResponse { Available = true, StaffAvailable = true };
    }

    [HttpPost(Name = "submit-application")]
    public async Task<ActionResult> Submit(ApplicationSubmissionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var success = await _applicationStore.SaveApplicationData(request);

        // TODO: add success message to response
        return success ? Ok() : BadRequest();
    }
}
