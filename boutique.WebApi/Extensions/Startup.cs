using System.Text;
using System.Text.Json.Serialization;
using boutique.Application.Interfaces.Common;
using boutique.WebApi.Middlewares;
using boutique.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

using boutique.Infraestructure;
using boutique.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace boutique.WebApi.Extensions;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Cors Service
        services.AddCors(o => o.AddPolicy("corsApp", builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }));
        
        services.AddHttpContextAccessor();
            
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        
        services.AddInfraestructure();
        
        services.AddApplicationLayer();
        
        services.AddScoped<IUsuarioSession, UsuarioSession>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]!)),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });
        
        services.TryAddSingleton<ISystemClock, SystemClock>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseCors("corsApp");

        app.UseMiddleware<CustomMiddleware>();
            
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
            
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "DELFOSTI API V1");
            c.RoutePrefix = string.Empty;
        });
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}