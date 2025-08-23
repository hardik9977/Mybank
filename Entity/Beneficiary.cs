using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entity;

/// <summary>
/// Represents a beneficiary entity, mapping to the 'beneficiaries' table in the database.
/// </summary>
[Table("beneficiaries")]
public class Beneficiary
{
    /// <summary>
    /// The unique identifier for the beneficiary. Maps to 'id'.
    /// </summary>
    [Key]
    [Column("id")]
    public long Id { get; set; }

    /// <summary>
    /// The foreign key to the user who added the beneficiary. Maps to 'user_id'.
    /// </summary>
    [Column("user_id")]
    public int UserId { get; set; }

    /// <summary>
    /// The beneficiary's account number. Maps to 'beneficiary_account_number'.
    /// </summary>
    [Column("beneficiary_account_number")]
    [ForeignKey("Account")]
    public string BeneficiaryAccountNumber { get; set; }

    /// <summary>
    /// A friendly nickname for the beneficiary. Maps to 'nickname'.
    /// </summary>
    [Column("nickname")]
    public string Nickname { get; set; }

    /// <summary>
    /// The timestamp when the beneficiary was added. Maps to 'added_at'.
    /// </summary>
    [Column("added_at")]
    public DateTime AddedAt { get; set; }

    /// <summary>
    /// Navigation property for the User entity.
    /// </summary>
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    /// <summary>
    /// Navigation property for the Account entity.
    /// </summary>
    [ForeignKey("BeneficiaryAccountNumber")]
    public virtual Account Account { get; set; }
}