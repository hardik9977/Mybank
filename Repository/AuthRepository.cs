using System;
using System.Threading.Tasks;
using API.Context;
using API.Dtos.Auth;
using API.Entity;
using API.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class AuthRepository
{
    private readonly DBContext _context;
    private readonly PasswordService _passwordService;

    private readonly LogingService _logingService;

    private readonly JwtService _jwtService;

    public AuthRepository(DBContext context, PasswordService passwordService, JwtService jwtService, LogingService logingService)
    {
        _logingService = logingService;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _context = context;
    }

    public async Task<string> RegisterUser(RegisterDto registerDto)
    {
        User user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            Password = _passwordService.HashPassword(registerDto.Password), // Ensure to hash the password before saving
            CreatedAt = registerDto.CreatedAt,
            UpdatedAt = registerDto.UpdatedAt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return "User registered successfully";
    }

    public async Task<string> LoginUser(LoginDto loginDto)
    {
        User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null || !_passwordService.VerifyPassword(loginDto.Password, user.Password))
        {
            return "Invalid email or password";
        }


        // Logic for generating JWT token can be added here
        string token = _jwtService.GenerateToken(user);

        return token;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
