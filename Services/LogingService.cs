using System;
using System.Threading.Tasks;
using API.Context;
using API.Entity;

namespace API.Services;

public class LogingService
{
    readonly DBContext _dbContext;

    public LogingService(DBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task LogLoginAttempt(int userId, string ipAddress, string deviceInfo)
    {
        // Logic to log the login attempt, e.g., saving to a database
        var loginLog = new LoginLogs
        {
            UserId = userId,
            LoginTime = DateTime.UtcNow,
            IpAddress = ipAddress,
            DeviceInfo = deviceInfo
        };
        _dbContext.LoginLogs.Add(loginLog);
        await _dbContext.SaveChangesAsync();
    }

}
