using System;
using API.Entity;

namespace API.Dtos.Transaction;

public class CreateTransactionDto
{
    /// <summary>
    /// The account number associated with the transaction.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// The amount involved in the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The type of the transaction (DEBIT or CREDIT).
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// A description of the transaction.
    /// </summary>
    public string Description { get; set; }

}
