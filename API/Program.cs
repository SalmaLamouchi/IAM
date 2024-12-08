using DAL;
using DAL.Entities;
using DAL.IRepository;
using DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Service;
using Services.Common.Mappings;
using Services.DTO;
using Services.IService;
using Services.Service;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Configurer Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSingleton<ILogger>(Log.Logger);

builder.Host.UseSerilog();
builder.Logging.AddSerilog();

// Ajouter les services
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Ajouter la configuration de la cha�ne de connexion et du DbContext
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDbConnection")));

builder.Services.AddService(builder.Configuration);

// Enregistrer les services sp�cifiques
builder.Services.AddScoped(typeof(IServiceAsync<,>), typeof(ServiceAsync<,>));

builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", policy =>
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true));
});

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IServiceAsync<Menu, MenuDto>, ServiceAsync<Menu, MenuDto>>();
builder.Services.AddScoped<IRepositoryAsync<Menu>, RepositoryAsync<Menu>>();

// R�cup�rer la cl� secr�te pour les services Token
var tokenSecretKey = builder.Configuration["TokenSettings:SecretKey"]; // Acc�s � la cl� secr�te depuis le fichier appsettings.json

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
// Enregistrer les services avec l'injection de d�pendances
builder.Services.AddScoped<ITokenService>(provider => new TokenService(tokenSecretKey)); // Injection de la cl� secr�te dans TokenService
builder.Services.AddScoped<ITokenService>(provider => new InMemoryTokenService(tokenSecretKey)); // Injection de la cl� secr�te dans InMemoryTokenService

var app = builder.Build(); // La seule fois o� vous appelez Build()

// Middleware global pour capturer les exceptions non g�r�es
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Log.Error("Une erreur non g�r�e s'est produite : {Message}", ex.Message);
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Erreur interne du serveur.");
    }
});

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseCors("CORSPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
