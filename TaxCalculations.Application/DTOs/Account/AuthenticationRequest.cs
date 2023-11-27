using System.ComponentModel.DataAnnotations;

namespace TaxCalculations.Application.DTOs.Account;

public record AuthenticationRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}