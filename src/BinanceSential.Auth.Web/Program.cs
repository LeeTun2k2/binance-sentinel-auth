using BinanceSential.Auth.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

builder.AddLoggerConfigs();

var appLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddOptionConfigs(builder.Configuration, appLogger, builder);
builder.Services.AddServiceConfigs(appLogger, builder);

var allowFeConnection = "AllowFeConnection";
string[] allowedOrigins = builder.Configuration
  .GetSection("Cors:AllowedOrigins")
  .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: allowFeConnection, policy =>
  {
    policy.WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
});

if (builder.Environment.IsProduction())
{
  builder.Services.AddFastEndpoints();
}
else
{
  builder.Services.AddFastEndpoints().SwaggerDocument(o =>
  {
    o.ShortSchemaNames = true;
  });
}

var app = builder.Build();

await app.UseAppMiddlewareAndSeedDatabase();

app.UseCors(allowFeConnection);
app.UseAuthentication();
app.UseAuthorization();

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program { }
