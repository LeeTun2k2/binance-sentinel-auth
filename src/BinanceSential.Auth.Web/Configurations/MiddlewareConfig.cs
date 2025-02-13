using Ardalis.ListStartupServices;
using BinanceSential.Auth.Core.UserAggregate;
using BinanceSential.Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace BinanceSential.Auth.Web.Configurations;

public static class MiddlewareConfig
{
  public static async Task<IApplicationBuilder> UseAppMiddlewareAndSeedDatabase(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
    }
    else
    {
      app.UseDefaultExceptionHandler(); // from FastEndpoints
      app.UseHsts();
    }

    app.UseFastEndpoints()
        .UseSwaggerGen(); // Includes AddFileServer and static files middleware

    app.UseHttpsRedirection(); // Note this will drop Authorization headers

    await SeedDatabase(app);

    return app;
  }

  static async Task SeedDatabase(WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
      var context = services.GetRequiredService<AppDbContext>();
      var configuration = services.GetRequiredService<IConfiguration>();
      var userManager = services.GetRequiredService<UserManager<User>>();
      var roleManager = services.GetRequiredService<RoleManager<Role>>();
      context.Database.EnsureCreated();
      await SeedData.InitializeAsync(configuration, context, userManager, roleManager);
    }
    catch (Exception ex)
    {
      var logger = services.GetRequiredService<ILogger<Program>>();
      logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
    }
  }
}
