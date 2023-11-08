using System.Reflection;
using boutique.Application.Interfaces.Services;
using boutique.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace boutique.Application;

public static class ConfigureServices
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUtilService, UtilService>();
        services.AddScoped<IProductoService, ProductoService>();
    }
}