using System;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos.Account;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly AccountRepository _accountRepository;

    public AccountController(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccount()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var account = await _accountRepository.GetAccountByUserIdAsync(userId);

        if (account == null)
        {
            return NotFound("Account not found.");
        }
        // Assuming you want to return a list of accounts
        List<AccountDto> accountDtos = account.Select(a => new AccountDto
        {
            AccountNumber = a.AccountNumber,
            AccountType = a.AccountType,
            Currency = a.Currency,
            Status = a.Status,
            Balance = a.Balance
        }).ToList();

        return Ok(accountDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto createAccountDto)
    {
        if (createAccountDto == null)
        {
            return BadRequest("Invalid account data.");
        }

        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var account = await _accountRepository.CreateAccountAsync(createAccountDto, userId);

        return Ok(new { Message = "Account created successfully", Account = account });
    }

    [HttpPut("{accountId}")]
    public async Task<IActionResult> UpdateAccount(string accountId, [FromBody] UpdateAccountDto updateAccountDto)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        if (updateAccountDto == null)
        {
            return BadRequest("Invalid account data.");
        }

        bool isUpdated = await _accountRepository.UpdateAccountAsync(accountId, updateAccountDto, userId);
        if (!isUpdated)
        {
            return NotFound("Account not found.");
        }

        return Ok("Account updated successfully.");
    }
}
