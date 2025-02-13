using BinanceSential.Auth.Core.Interfaces.ITokenService;
using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.Infrastructure.Data;
using BinanceSential.Auth.Infrastructure.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BinanceSential.Auth.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    // Add database context
    string? connectionString = config.GetConnectionString("DefaultConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

    // Add identity
    services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

    // Add token services
    var jwtConfig = new JwtConfig(config);
    services.AddAuthentication()
      .AddJwtBearer(jwtOptions =>
      {
        jwtOptions.Authority = jwtConfig.Issuer;
        jwtOptions.Audience = jwtConfig.Audience;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateIssuerSigningKey = true,
          ValidIssuers = jwtConfig.ValidIssuers,
          ValidAudiences = jwtConfig.ValidAudiences,
          IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
        };

        jwtOptions.MapInboundClaims = false;
      });
    services.AddAuthorization();
    services.AddHttpContextAccessor();
    services.AddSingleton<IJwtService, JwtService>();

    // Add repositories
    services
      .AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
      .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
