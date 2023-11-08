using boutique.Domain.Entities;

namespace boutique.Application.Interfaces.Authentication;

public interface IJwtGenerator
{
    string GenerateToken(Usuario usuario);
}