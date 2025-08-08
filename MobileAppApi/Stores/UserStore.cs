using Microsoft.EntityFrameworkCore;
using MobileAppApi.Models.Db;
using MobileAppApi.Models.Network;

namespace MobileAppApi.Stores;
public class UserStore(ILogger<UserStore> logger, MobileApiContext mobileApiContext)
{
    private readonly ILogger<UserStore> _logger = logger;
    private readonly MobileApiContext _mobileApiContext = mobileApiContext;

    public async Task<string?> ClientCountryCode(string clientNumber)
    {
        return await _mobileApiContext.Clients
                    .Where(c => c.ClientNumber == clientNumber)
                    .Select(c => c.A2countryCode)
                    .FirstOrDefaultAsync();
    }

    // TODO: create response type to convey various success and failure options.
    public async Task<bool> RegisterDevice(RegistrationRequest request)
    {
        var user = await _mobileApiContext.Users
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

        using var transaction = _mobileApiContext.Database.BeginTransaction();

        try
        {
            _mobileApiContext.SaveChanges();
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
