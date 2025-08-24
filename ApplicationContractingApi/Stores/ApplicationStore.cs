using ApplicationContractingApi.Models.Db;
using ApplicationContractingApi.Models.Network;

namespace ApplicationContractingApi.Stores;
public class ApplicationStore(ILogger<ApplicationStore> logger, ApplicationContractingApiContext apiContext)
{
    private readonly ILogger<ApplicationStore> _logger = logger;
    private readonly ApplicationContractingApiContext _apiContext = apiContext;

    // TODO: Consider a different return type to differentiate success from duplicate submissions.
    public async Task<bool> SaveApplicationData(ApplicationSubmissionRequest request)
    {
        // Handle duplicate submissions. A duplicate submission could occur due to network conditions preventing the
        // API consumer from receiving the APIs response.
        if (_apiContext.ApplicationSubmissions.Any(c => c.SubmissionId == request.SubmissionId))
        {
            return true;
        }

        var fields = request.Fields.ConvertAll(f => new ApplicationFieldSubmission
        {
            FieldNamespace = f.FieldNamespace,
            FieldName = f.FieldName,
            FieldValue = f.FieldValue,
            LabelText = f.LabelText,
            SelectedItemText = f.SelectedItemText,
        });

        var newSubmission = new ApplicationSubmission
        {
            SubmissionId = request.SubmissionId,
            ApplicationType = "Scholarship", // TODO: replace with static string enum or create application type table
            UserId = AppConstants.ClientId,
            ApplicationFieldSubmissions = fields,
        };

        _apiContext.ApplicationSubmissions.Add(newSubmission);

        using var transaction = _apiContext.Database.BeginTransaction();

        try
        {
            await _apiContext.SaveChangesAsync();
            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Failed to save application submission for {SubmissionId}", request.SubmissionId);
            return false;
        }
    }
}
