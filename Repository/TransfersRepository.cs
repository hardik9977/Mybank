using System;
using System.Threading.Tasks;
using API.Context;
using API.Dtos.Transfer;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class TransfersRepository
{
    // This class can be used to implement methods related to transfers between accounts.
    // For example, methods to initiate a transfer, check transfer status, etc.

    private readonly DBContext _context;
    private readonly AccountRepository _accountRepository;
    private readonly TransactionRepository _transactionRepository;

    public TransfersRepository(DBContext context, AccountRepository accountRepository, TransactionRepository transactionRepository)
    {
        _context = context;
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    // Example method signature:
    public async Task InitiateTransfer(CreateTransferDto transferDto)
    {
        // Logic to initiate a transfer
        string fromAccountNumber = transferDto.FromAccountNumber;
        string toAccountNumber = transferDto.ToAccountNumber;
        if (string.IsNullOrEmpty(fromAccountNumber) || string.IsNullOrEmpty(toAccountNumber))
        {
            throw new Exception("Both account numbers must be provided.");
        }
        if (transferDto.Amount <= 0)
        {
            throw new Exception("Transfer amount must be greater than zero.");
        }
        if (transferDto.FromAccountNumber == transferDto.ToAccountNumber)
        {
            throw new Exception("Cannot transfer to the same account.");
        }

        Account fromAccount = await _accountRepository.GetAccountByAccountNumberAsync(fromAccountNumber);
        Account toAccount = await _accountRepository.GetAccountByAccountNumberAsync(toAccountNumber);
        if (fromAccount == null || toAccount == null)
        {
            throw new Exception("One or both accounts not found.");
        }
        if (fromAccount.Balance < transferDto.Amount)
        {
            throw new Exception("Insufficient balance for transfer.");
        }

        // Create a transfer record
        Transfer transfer = new Transfer
        {
            FromAccountNumber = fromAccountNumber,
            ToAccountNumber = toAccountNumber,
            Amount = transferDto.Amount,
            ReferenceNote = transferDto.ReferenceNote,
        };
        _context.Transfers.Add(transfer);

        // Create a transaction for the debit from the sender's account
        Transaction debitTransaction = new Transaction
        {
            AccountNumber = fromAccountNumber,
            Amount = transferDto.Amount,
            Type = TransactionType.DEBIT,
            Description = transferDto.ReferenceNote + " (Transfer to " + toAccountNumber + ")",
            BalanceAfterTransaction = fromAccount.Balance - transferDto.Amount,
            TransactionDate = DateTime.UtcNow
        };
        _context.Transactions.Add(debitTransaction);
        // Update the sender's account balance
        fromAccount.Balance = debitTransaction.BalanceAfterTransaction;
        _context.Accounts.Update(fromAccount);

        // Create a transaction for the credit to the receiver's account
        Transaction creditTransaction = new Transaction
        {
            AccountNumber = toAccountNumber,
            Amount = transferDto.Amount,
            Type = TransactionType.CREDIT,
            Description = transferDto.ReferenceNote + " (Transfer from " + fromAccountNumber + ")",
            BalanceAfterTransaction = toAccount.Balance + transferDto.Amount,
            TransactionDate = DateTime.UtcNow
        };
        _context.Transactions.Add(creditTransaction);
        // Update the receiver's account balance
        toAccount.Balance = creditTransaction.BalanceAfterTransaction;
        _context.Accounts.Update(toAccount);

        // Save all changes to the database
        await _context.SaveChangesAsync();
    }


}
