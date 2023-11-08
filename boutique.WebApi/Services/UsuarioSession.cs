using System.Security.Claims;
using boutique.Application.Interfaces.Common;

namespace boutique.WebApi.Services;

public class UsuarioSession : IUsuarioSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioSession(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUsuarioSession()
    {
        return _httpContextAccessor
            .HttpContext
            .User?
            .Claims?
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
            .Value;
    }
}