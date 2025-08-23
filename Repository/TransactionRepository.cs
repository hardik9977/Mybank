using System;
using System.Threading.Tasks;
using API.Context;
using API.Dtos.Transaction;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class TransactionRepository
{
    // This class will contain methods to interact with the Transaction entity
    // For example, methods to create, read, update, and delete transactions
    // It will use the DbContext to perform database operations

    // Example method signature:
    private readonly DBContext _context;

    private readonly AccountRepository _accountRepository;
    public TransactionRepository(DBContext context, AccountRepository accountRepository)
    {
        _context = context;
        _accountRepository = accountRepository;
    }
    public async Task AddTransaction(CreateTransactionDto transactionDto)
    {
        // Logic to create a new transaction from the DTO and save it to the database

        string accountNumber = transactionDto.AccountNumber;

        Account account = await _accountRepository.GetAccountByAccountNumberAsync(accountNumber);

        if (account == null)
        {
            throw new Exception("Account not found");
        }

        if (transactionDto.Amount <= 0)
        {
            throw new Exception("Transaction amount must be greater than zero");
        }
        if (transactionDto.Type == TransactionType.DEBIT && account.Balance < transactionDto.Amount)
        {
            throw new Exception("Insufficient balance for debit transaction");
        }

        Transaction transaction = new Transaction
        {
            AccountNumber = transactionDto.AccountNumber,
            Amount = transactionDto.Amount,
            Type = transactionDto.Type,
            Description = transactionDto.Description,
            BalanceAfterTransaction = account.Balance + (transactionDto.Type == TransactionType.CREDIT ? transactionDto.Amount : -transactionDto.Amount),
            TransactionDate = DateTime.UtcNow
        };

        _context.Transactions.Add(transaction);
        // Update the account balance
        account.Balance = transaction.BalanceAfterTransaction;
        _context.Accounts.Update(account);
        _context.SaveChanges();
    }
    public async Task<IEnumerable<Transaction>> GetTransactionsByAccountNumberAsync(string accountNumber)
    {
        // Logic to retrieve transactions by account number
        return await _context.Transactions
            .Where(t => t.AccountNumber == accountNumber).ToListAsync();
    }

}
