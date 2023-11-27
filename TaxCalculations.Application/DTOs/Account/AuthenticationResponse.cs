using System.Text.Json.Serialization;

namespace TaxCalculations.Application.DTOs.Account;

public record AuthenticationResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string JWToken { get; set; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
}
