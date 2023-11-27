using TaxCalculations.Application.DTOs.Account;
using TaxCalculations.Application.Wrappers;

namespace TaxCalculations.Application.Contracts.Services;

public interface IAccountService
{
    Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
    Task<Response<TokenModel>> RefreshToken(TokenModel tokenModel, string ipAddress);
}
