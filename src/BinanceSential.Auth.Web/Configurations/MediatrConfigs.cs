using System.Reflection;
using Ardalis.SharedKernel;
using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.UseCases.Authentications.Register;

namespace BinanceSential.Auth.Web.Configurations;

public static class MediatrConfigs
{
  public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
  {
    var mediatRAssemblies = new[]
      {
        Assembly.GetAssembly(typeof(User)), // Core
        Assembly.GetAssembly(typeof(RegisterCommand)) // UseCases
      };

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

    return services;
  }
}
