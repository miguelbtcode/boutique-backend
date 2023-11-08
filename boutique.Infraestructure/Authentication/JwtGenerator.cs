using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using boutique.Application.Interfaces.Authentication;
using boutique.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace boutique.Infraestructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.GivenName, usuario.Nombre!),
            new Claim(ClaimTypes.Email, usuario.CorreoElectronico!)
        };
            
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires =  DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials
        };
            
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}