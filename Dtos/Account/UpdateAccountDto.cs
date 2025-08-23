using System;
using API.Entity;

namespace API.Dtos.Account;

public class UpdateAccountDto
{
    public AccountType AccountType { get; set; } // Nullable to allow partial updates
    public string Currency { get; set; } // Nullable to allow partial updates
    public AccountStatus Status { get; set; } // Nullable to allow partial updates

}
