using ApplicationContractingApi.Models.Network;
using ApplicationContractingApi.Stores;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationContractingApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RegistrationController(ILogger<RegistrationController> logger, UserStore userStore) : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger = logger;
    private readonly UserStore _userStore = userStore;

    [HttpGet(Name = "preflight/{clientNumber}")]
    public async Task<ActionResult> Get(string clientNumber)
    {
        ArgumentNullException.ThrowIfNull(clientNumber);

        string? clientCountryCode = await _userStore.ClientCountryCode(clientNumber);

        if (clientCountryCode == null)
        {
            return BadRequest();
        }

        return Ok(new RegistrationPreflightResponse { A2CountryCode = clientCountryCode });
    }

    [HttpPost()]
    public async Task<ActionResult> Post(RegistrationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return Ok();
    }

}
