using System.ComponentModel.DataAnnotations;

namespace TaxCalculations.Application.DTOs.Account;

public record RegisterRequest
{

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}
