using System;

namespace API.Entity;

public class LoginLogs
{
    public int Id { get; set; }
    public DateTime LoginTime { get; set; }
    public string IpAddress { get; set; }
    public string DeviceInfo { get; set; }

    // Navigation property to link to User entity
    public int UserId { get; set; }
    public User User { get; set; }

}
