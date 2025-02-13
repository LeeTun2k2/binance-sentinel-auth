using BinanceSential.Auth.Core.UserAggregate;
using Microsoft.AspNetCore.Identity;
namespace BinanceSential.Auth.Infrastructure.Data;

public static class SeedData
{
  public static async Task InitializeAsync(IConfiguration configuration, AppDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
  {
    if (await dbContext.Users.AnyAsync()) return;

    await SeedRoles(roleManager);
    await SeedAdminAccount(configuration, userManager);
  }
  private static async Task SeedRoles(RoleManager<Role> roleManager)
  {
    // Create Admin role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
      var adminRole = new Role { Name = "Admin" };
      await roleManager.CreateAsync(adminRole);
    }

    // Create User role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("User"))
    {
      var userRole = new Role { Name = "User" };
      await roleManager.CreateAsync(userRole);
    }
  }

  public static async Task SeedAdminAccount(IConfiguration configuration, UserManager<User> userManager)
  {
    // Fetch admin details from configuration
    var adminEmail = configuration["InitialAccount:Email"];
    var adminPassword = configuration["InitialAccount:Password"];
    var adminUsername = configuration["InitialAccount:Username"];

    // Validate configuration
    if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword) || string.IsNullOrWhiteSpace(adminUsername))
    {
      throw new Exception("Failed to start project. Missing aplication settings for Initial Account");
    }

    // Check if the default admin user exists; if not, create it.
    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
      user = new User
      {
        UserName = adminUsername,
        Email = adminEmail,
      };
      var result = await userManager.CreateAsync(user, adminPassword); // Use password from configuration
      if (result.Succeeded)
      {
        // Assign the Admin role to this user
        await userManager.AddToRoleAsync(user, "Admin");
      }
    }
  }
}
