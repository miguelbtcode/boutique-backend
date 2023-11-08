using boutique.Application.Interfaces.Authentication;
using boutique.Application.Interfaces.Common;
using boutique.Infraestructure.Authentication;
using boutique.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace boutique.Infraestructure;

public static class ConfigureServices
{
    public static void AddInfraestructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("BoutiqueDatabase"));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
    }
}