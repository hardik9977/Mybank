using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entity;

/// <summary>
/// Represents the status of a transfer, mapping to the 'status' ENUM in the database.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransferStatus
{
    PENDING,
    SUCCESS,
    FAILED
}
/// <summary>
/// Represents a transfer entity, mapping to the 'transfers' table in the database.
/// </summary>
[Table("transfers")]
public class Transfer
{
    /// <summary>
    /// The unique identifier for the transfer. Maps to 'id'.
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// The foreign key to the sending account. Maps to 'from_account_id'.
    /// </summary>
    [Column("from_account_Number")]
    public string FromAccountNumber { get; set; }

    /// <summary>
    /// The foreign key to the receiving account. Maps to 'to_account_id'.
    /// </summary>
    [Column("to_account_Number")]
    public string ToAccountNumber { get; set; }

    /// <summary>
    /// The amount of money transferred. Maps to 'amount'.
    /// </summary>
    [Column("amount", TypeName = "decimal(15, 2)")]
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// A note or description for the transfer. Maps to 'reference_note'.
    /// </summary>
    [Column("reference_note")]
    [MaxLength(255)]
    public string ReferenceNote { get; set; }

    /// <summary>
    /// The current status of the transfer. Maps to 'status'.
    /// </summary>
    [Column("status")]
    public TransferStatus Status { get; set; } = TransferStatus.PENDING;

    /// <summary>
    /// The timestamp when the transfer was created. Maps to 'created_at'.
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Navigation property for the sending Account entity.
    /// </summary>
    [ForeignKey("FromAccountNumber")]
    public virtual Account FromAccount { get; set; }

    /// <summary>
    /// Navigation property for the receiving Account entity.
    /// </summary>
    [ForeignKey("ToAccountNumber")]
    public virtual Account ToAccount { get; set; }

}
