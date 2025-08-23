using System;

namespace API.Dtos.Auth;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
