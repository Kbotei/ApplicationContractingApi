using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;

namespace MobileAppApi.Stores;
public class ApplicationStore(ILogger<ApplicationStore> logger, MobileApiContext mobileApiContext)
{
    private readonly ILogger<ApplicationStore> _logger = logger;
    private readonly MobileApiContext _mobileApiContext = mobileApiContext;

    // TODO: Consider a different return type to differentiate success from duplicate submissions.
    public async Task<bool> SaveApplicationData(ApplicationSubmissionRequest request)
    {
        // Handle duplicate submissions. A duplicate submission could occur due to network conditions preventing the
        // API consumer from receiving the APIs response.
        if (_mobileApiContext.ApplicationSubmissions.Any(c => c.SubmissionId == request.SubmissionId))
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
            ClientId = AppConstants.ClientId,
            DeviceId = AppConstants.DeviceId,
            ApplicationFieldSubmissions = fields,
        };

        _mobileApiContext.ApplicationSubmissions.Add(newSubmission);

        using var transaction = _mobileApiContext.Database.BeginTransaction();

        try
        {
            await _mobileApiContext.SaveChangesAsync();
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
