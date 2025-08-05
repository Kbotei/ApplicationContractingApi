using MobileAppApi.Controllers;
using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;

namespace MobileAppApi.Stores
{
    public class AccountStore(ILogger<AccountStore> logger, MobileApiContext mobileApiContext)
    {
        private readonly ILogger<AccountStore> _logger = logger;
        private readonly MobileApiContext _mobileApiContext = mobileApiContext;

        public async Task<bool> SaveContractingData(ContractingSubmissionRequest request)
        {
            if (_mobileApiContext.ContractingSubmissions.Any(c => c.SubmissionId == request.SubmissionId))
            {
                return true;
            }

            var fields = request.Fields.ConvertAll(f => new ContractingFieldSubmission
            {
                FieldNamespace = f.FieldNamespace,
                FieldName = f.FieldName,
                FieldValue = f.FieldValue,
            });

            var contractingSubmission = new ContractingSubmission
            {
                SubmissionId = request.SubmissionId,
                ClientId = Guid.Parse("d33f9df0-e8c1-47bd-bf1a-25713680cce2"),
                DeviceId = Guid.Parse("aabddf66-7c41-4a6d-94d7-a9861249dcd5"),
                ContractingFieldSubmissions = fields
            };

            using var transaction = _mobileApiContext.Database.BeginTransaction();

            try
            {
                _mobileApiContext.ContractingSubmissions.Add(contractingSubmission);
                await _mobileApiContext.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                _logger.LogError("Failed to save contracting submission for {0}", request.SubmissionId);
                return false;
            }
        }
    }
}
