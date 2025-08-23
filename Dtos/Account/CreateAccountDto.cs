using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using API.Entity;

namespace API.Dtos.Account;

public class CreateAccountDto
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AccountType AccountType { get; set; } // e.g., "Savings", "Checking"

    [MaxLength(3)]
    public string Currency { get; set; } // e.g., "USD", "EUR"
}
