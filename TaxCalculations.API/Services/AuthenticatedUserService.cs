using System.Security.Claims;
using TaxCalculations.Application.Contracts.Services;

namespace TaxCalculations.API.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
    }

    public string UserId { get; }
}
