using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileAppApi.Models;
using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;
using MobileAppApi.Stores;

namespace MobileAppApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContractingController(ILogger<ContractingController> logger, MobileApiContext mobileApiContext, ContractingStore contractingStore) : ControllerBase
{
    private readonly ILogger<ContractingController> _logger = logger;
    private readonly MobileApiContext _mobileApiContext = mobileApiContext;
    private readonly ContractingStore _contractingStore = contractingStore;

    [HttpGet(Name = "account-data/{accountNumber}")]
    public async Task<ActionResult> Get(string accountNumber)
    {
        ArgumentNullException.ThrowIfNull(accountNumber);

        var account = await _mobileApiContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        if (account == null)
        {
            return BadRequest();
        }

        // TODO: retrieve account data and fields from db or backend service.

        var accountResponse = new ContractingAccountDataResponse
        {
            DocumentsInFlight = true,
            Applicants = [
                new Applicant {
                    applicantType = ApplicantType.Primary,
                    FirstName = "John",
                    LastName = "Doe",
                },
                ],
            AccountFields = [
                new SimpleField {
                    FieldNamespace = "Account",
                    FieldName = "AmountFinanced",
                    FieldValue = "2303.23",
                },
                ],
        };

        return Ok(accountResponse);
    }

    [HttpPost(Name = "create-contract")]
    public async Task<ActionResult> Post(ContractingSubmissionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var success = await _contractingStore.SaveContractingData(request);

        // After saving to database we would want to kick off a command to an endpoint via something like NServiceBus.
        return success ? Ok() : BadRequest();
    }
}
