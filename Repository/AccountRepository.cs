using System;
using API.Context;
using API.Dtos.Account;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class AccountRepository
{
    private readonly DBContext _context;

    public AccountRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<List<Account>> GetAccountByUserIdAsync(int userId)
    {
        return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync();
    }

    public async Task<Account> CreateAccountAsync(CreateAccountDto createAccountDto, int userId)
    {
        Account account = new Account
        {
            AccountType = createAccountDto.AccountType,
            Currency = createAccountDto.Currency,
            AccountNumber = GenerateAccountNumber(), // Assuming a method to generate unique account number
            UserId = userId,
            Balance = 0, // Initial balance can be set to zero or a default value
        };

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<Account> GetAccountByIdAsync(string accountNumber, int userId)
    {
        // Ensure the account belongs to the user
        return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber && a.UserId == userId);
    }

    public async Task<bool> UpdateAccountAsync(string accountId, UpdateAccountDto updateAccountDto, int userId)
    {
        var account = await GetAccountByIdAsync(accountId, userId);
        if (account == null)
        {
            return false;
        }

        account.AccountType = updateAccountDto.AccountType;
        account.Currency = updateAccountDto.Currency;
        account.Status = updateAccountDto.Status;

        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Account> GetAccountByAccountNumberAsync(string accountNumber)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }

    private string GenerateAccountNumber()
    {
        // Generate unique account number logic
        return DateTime.UtcNow.Ticks.ToString();
    }


}
