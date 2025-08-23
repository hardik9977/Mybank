using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entity;

/// <summary>
/// Represents the transaction type, mapping to the 'type' ENUM in the database.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionType
{
    DEBIT,
    CREDIT
}

[Table("transactions")]
public class Transaction
{
    /// <summary>
    /// The unique identifier for the transaction. Maps to 'id'.
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// The foreign key to the account associated with the transaction. Maps to 'account_id'.
    /// </summary>
    [Column("account_number")]
    public string AccountNumber { get; set; }

    /// <summary>
    /// The type of the transaction (DEBIT or CREDIT). Maps to 'type'.
    /// </summary>
    [Column("type")]
    [Required]
    public TransactionType Type { get; set; }

    /// <summary>
    /// The amount of the transaction. Maps to 'amount'.
    /// </summary>
    [Column("amount", TypeName = "decimal(15, 2)")]
    public decimal Amount { get; set; }

    /// <summary>
    /// A description for the transaction. Maps to 'description'.
    /// </summary>
    [Column("description")]
    [MaxLength(255)]
    public string Description { get; set; }

    /// <summary>
    /// The timestamp when the transaction occurred. Maps to 'transaction_date'.
    /// </summary>
    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The account balance after the transaction. Maps to 'balance_after_transaction'.
    /// </summary>
    [Column("balance_after_transaction", TypeName = "decimal(15, 2)")]
    public decimal BalanceAfterTransaction { get; set; }

    /// <summary>
    /// Navigation property for the Account entity.
    /// </summary>
    [ForeignKey("account_number")]
    public virtual Account Account { get; set; }
}
