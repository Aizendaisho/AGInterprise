using AGInterprise.Domain.Entities.Seguridad;
using AGInterprise.WebApi.Models.Auth;

namespace AGInterprise.WebApi.Services;

public interface IAuthService
{
    Task<string> LoginAsync(LoginRequest request);
    Task RegisterAsync(RegisterRequest request);
}
