using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace API.Entity;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccountType
{
    SAVINGS,
    CHECKING
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccountStatus
{
    ACTIVE,
    INACTIVE
}

[Index(nameof(AccountNumber), IsUnique = true)]
public class Account
{
    [Key]
    public int Id { get; set; }
    public string AccountNumber { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AccountType AccountType { get; set; }

    [Required]
    [MaxLength(3)]
    public string Currency { get; set; } = "INR";

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AccountStatus Status { get; set; } = AccountStatus.ACTIVE;

    [Column(TypeName = "decimal(15,2)")]
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property to link to User entity
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; }

}
