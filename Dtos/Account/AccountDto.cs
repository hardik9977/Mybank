using System;
using API.Entity;

namespace API.Dtos.Account;

public class AccountDto
{
    public string AccountNumber { get; set; } // Unique identifier for the account
    public AccountType AccountType { get; set; } // e.g., "Savings", "Checking"
    public string Currency { get; set; } // e.g., "USD", "EUR"
    public AccountStatus Status { get; set; } // e.g., "Active", "Inactive"
    public decimal Balance { get; set; } // Current balance in the account

}
