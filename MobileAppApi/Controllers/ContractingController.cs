using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileAppApi.Models;
using MobileAppApi.Models.Db;
using MobileAppApi.NetworkModels;

namespace MobileAppApi.Controllers
{
    public class ContractingController(ILogger<ContractingController> logger, MobileApiContext mobileApiContext) : ControllerBase
    {
        private readonly ILogger<ContractingController> _logger = logger;
        private readonly MobileApiContext _mobileApiContext = mobileApiContext;

        [HttpGet(Name = "account-data/{accountNumber}")]
        public async Task<ActionResult> Get(string accountNumber)
        {
            var account = await _mobileApiContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult> Post()
        {
            return Ok();
        }
    }
}
