using BinanceSential.Auth.Core.Interfaces.IEmailServices;
using BinanceSential.Auth.Infrastructure;
using BinanceSential.Auth.Infrastructure.Email;

namespace BinanceSential.Auth.Web.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
  {
    services.AddInfrastructureServices(builder.Configuration, logger)
            .AddMediatrConfigs();

    if (builder.Environment.IsDevelopment())
    {
      builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
    }
    else
    {
      services.AddScoped<IEmailSender, MimeKitEmailSender>();
    }

    logger.LogInformation("{Project} services registered", "Mediatr and Email Sender");

    return services;
  }


}
