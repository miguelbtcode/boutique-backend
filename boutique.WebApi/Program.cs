using boutique.Infraestructure.Persistence;
using boutique.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration); // My custom startup class.

// Add services to the container.
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// I build a new service provider from the services collection
using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    try
    {
        var initialiser = serviceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initialiser.SeedAsync().Wait();
    }
    catch(Exception e)
    {
        var logging = serviceProvider.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "Error while seeding data");
    }
}


// Configure the HTTP request pipeline.
startup.Configure(app, app.Environment); 

app.Run();