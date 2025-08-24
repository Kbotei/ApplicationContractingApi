using ApplicationContractingApi.Models.Db;
using ApplicationContractingApi.Models.Network;

namespace ApplicationContractingApi.Stores;
public class ContractingStore(ILogger<ContractingStore> logger, ApplicationContractingApiContext apiContext)
{
    private readonly ILogger<ContractingStore> _logger = logger;
    private readonly ApplicationContractingApiContext _apiContext = apiContext;

    public async Task<bool> SaveContractingData(ContractingSubmissionRequest request)
    {
        if (_apiContext.ContractingSubmissions.Any(c => c.SubmissionId == request.SubmissionId))
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

        using var transaction = _apiContext.Database.BeginTransaction();

        try
        {
            _apiContext.ContractingSubmissions.Add(contractingSubmission);
            await _apiContext.SaveChangesAsync();
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
