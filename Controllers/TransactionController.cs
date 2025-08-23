using System;
using API.Dtos.Transaction;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/transaction")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly TransactionRepository _transactionRepository;

    public TransactionController(TransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpPost("{accountNumber}/deposit")]
    public async Task<IActionResult> Deposit([FromBody] CreateTransactionDto transactionDto, string accountNumber)
    {
        if (transactionDto == null)
        {
            return BadRequest("Invalid transaction data.");
        }

        try
        {
            transactionDto.Type = Entity.TransactionType.CREDIT; // Default to CREDIT for deposits
            transactionDto.AccountNumber = accountNumber;
            await _transactionRepository.AddTransaction(transactionDto);
            return Ok(new { Message = "deposit successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating transaction: {ex.Message}");
        }
    }

    [HttpPost("{accountNumber}/withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] CreateTransactionDto transactionDto, string accountNumber)
    {
        if (transactionDto == null)
        {
            return BadRequest("Invalid transaction data.");
        }

        try
        {
            transactionDto.Type = Entity.TransactionType.DEBIT; // Default to CREDIT for deposits
            transactionDto.AccountNumber = accountNumber;
            await _transactionRepository.AddTransaction(transactionDto);
            return Ok(new { Message = "deposit successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating transaction: {ex.Message}");
        }
    }



    [HttpGet("{accountNumber}")]
    public async Task<IActionResult> GetTransactionsByAccountNumber(string accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            return BadRequest("Account number is required.");
        }

        try
        {
            var transactions = await _transactionRepository.GetTransactionsByAccountNumberAsync(accountNumber);
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error retrieving transactions: {ex.Message}");
        }
    }
}
