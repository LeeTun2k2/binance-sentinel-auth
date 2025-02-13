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

if (builder.Environment.IsDevelopment())
{
  builder.Services.AddFastEndpoints()
                  .SwaggerDocument(o =>
                  {
                    o.ShortSchemaNames = true;
                  });
}
var app = builder.Build();

await app.UseAppMiddlewareAndSeedDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program { }
