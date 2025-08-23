using System;

namespace API.Controllers;

using System.Threading.Tasks;
using API.Dtos.Auth;
using API.Entity;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    // This class will handle authentication-related actions
    // For example, login, logout, register, etc.

    private readonly AuthRepository _authService;
    private readonly JwtService _jwtService;

    private readonly LogingService _logingService;
    public AuthController(AuthRepository authService, JwtService jwtService, LogingService logingService)
    {
        _authService = authService;
        _jwtService = jwtService;
        _logingService = logingService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
        {
            return BadRequest("Email and password are required");
        }

        // Call the repository to handle login logic
        var result = await _authService.LoginUser(loginDto);
        if (result == "Invalid email or password")
        {
            return Unauthorized(result);
        }
        _jwtService.SetJwtCookie(Response, result, 10);

        var user = await _authService.GetUserByEmail(loginDto.Email);
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        var deviceInfo = HttpContext.Request.Headers["User-Agent"].ToString();
        await _logingService.LogLoginAttempt(user.Id, ip, deviceInfo);

        return Ok(new { Token = result, Message = "Login successful" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        User existingUser = await _authService.GetUserByEmail(registerDto.Email);
        if (existingUser != null)
        {
            return BadRequest("User with this email already exists");
        }
        string result = await _authService.RegisterUser(registerDto);
        return Ok("Registration successful");
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required");
        }

        User user = await _authService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [HttpGet("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // Logic to handle logout, e.g., clearing the JWT cookie
        _jwtService.ClearJwtCookie(Response);
        return Ok("Logged out successfully");
    }

}
