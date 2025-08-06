using MobileAppApi.Controllers;
using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;

namespace MobileAppApi.Stores
{
    public class AccountStore(ILogger<AccountStore> logger, MobileApiContext mobileApiContext)
    {
        private readonly ILogger<AccountStore> _logger = logger;
        private readonly MobileApiContext _mobileApiContext = mobileApiContext;

        public async Task<bool> SaveCreditApplicationData(CreditApplicationSubmissionRequest request)
        {
            // Handle duplicate submissions. A duplicate submission could occur due to network conditions preventing the
            // API consumer from receiving the APIs response.
            if (_mobileApiContext.CreditApplicationSubmissions.Any(c => c.SubmissionId == request.SubmissionId))
            {
                return true;
            }

            var fields = request.Fields.ConvertAll(f => new CreditApplicationFieldSubmission
            {
                FieldNamespace = f.FieldNamespace,
                FieldName = f.FieldName,
                FieldValue = f.FieldValue,
                LabelText = f.LabelText,
                SelectedItemText = f.SelectedItemText,
            });

            var newSubmission = new CreditApplicationSubmission
            {
                SubmissionId = request.SubmissionId,
                ClientId = AppConstants.ClientId,
                DeviceId = AppConstants.DeviceId,
                CreditApplicationFieldSubmissions = fields,
            };

            using var transaction = _mobileApiContext.Database.BeginTransaction();

            try
            {
                _mobileApiContext.CreditApplicationSubmissions.Add(newSubmission);
                await _mobileApiContext.SaveChangesAsync();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                _logger.LogError("Failed to save application submission for {0}", request.SubmissionId);
                return false;
            }
        }

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
                ClientId = AppConstants.ClientId,
                DeviceId = AppConstants.DeviceId,
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
