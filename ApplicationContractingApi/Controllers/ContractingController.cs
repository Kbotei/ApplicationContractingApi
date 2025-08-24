using ApplicationContractingApi.Models;
using ApplicationContractingApi.Models.Db;
using ApplicationContractingApi.Models.Network;
using ApplicationContractingApi.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationContractingApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContractingController(ILogger<ContractingController> logger, ApplicationContractingApiContext apiContext, ContractingStore contractingStore) : ControllerBase
{
    private readonly ILogger<ContractingController> _logger = logger;
    private readonly ApplicationContractingApiContext _apiContext = apiContext;
    private readonly ContractingStore _contractingStore = contractingStore;

    [HttpGet(Name = "account-data/{submissionId}")]
    public async Task<ActionResult> Get(Guid submissionId)
    {
        ArgumentNullException.ThrowIfNull(submissionId);

        var account = await _apiContext.ApplicationSubmissions.FirstOrDefaultAsync(a => a.SubmissionId == submissionId);
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
