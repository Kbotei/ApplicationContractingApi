using ApplicationContractingApi.Models.Db;
using ApplicationContractingApi.Models.Network;
using Microsoft.EntityFrameworkCore;

namespace ApplicationContractingApi.Stores;
public class UserStore(ILogger<UserStore> logger, ApplicationContractingApiContext apiContext)
{
    private readonly ILogger<UserStore> _logger = logger;
    private readonly ApplicationContractingApiContext _apiContext = apiContext;

    public async Task<string?> ClientCountryCode(string clientNumber)
    {
        return await _apiContext.Clients
                    .Where(c => c.ClientNumber == clientNumber)
                    .Select(c => c.A2countryCode)
                    .FirstOrDefaultAsync();
    }

    // TODO: create response type to convey various success and failure options.
    public async Task<bool> RegisterDevice(RegistrationRequest request)
    {
        var user = await _apiContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email) ?? 
            new User { 
                Email = request.Email,
                Password = "", // TODO: setup auth system to handle this
                PhoneNumber = request.PhoneNumber,
            };

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        var newDevice = new Device
        {
            OperatingSystem = request.OperatingSystem,
            OperatingSystemVersion = request.OperatingSystemVersion,
        };

        user.Devices.Add(newDevice);

        using var transaction = _apiContext.Database.BeginTransaction();

        try
        {
            _apiContext.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Failed to register new device for user {Email}", request.Email);
            return false;
        }
    }
}
