using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Entity;

[Index(nameof(Email), IsUnique = true)
, Index(nameof(PhoneNumber), IsUnique = true)]
public class User
{

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]

    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]

    public string LastName { get; set; }
    [Required]
    public string Password { get; set; }

    [EmailAddress]
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    [Phone]
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
