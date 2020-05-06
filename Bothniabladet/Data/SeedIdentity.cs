using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bothniabladet.Data
{
  public class SeedIdentity
  {
    public static async Task EnsuredPopulated(IServiceProvider services)
    {

      var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
      var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

      await CreateRoles(roleManager);
      await CreateUsers(userManager);
    }
    private static async Task CreateRoles(RoleManager<IdentityRole> rManager)
    {
      if (!await rManager.RoleExistsAsync("Admin"))
      {
        await rManager.CreateAsync(new IdentityRole("Admin"));
      }
    }

    public static async Task CreateUsers(UserManager<IdentityUser> uManager)
    {
      IdentityUser Admin = new IdentityUser
      {
        UserName = "Admin",
        Email = "Admin@admin.com"
      };
    
      await uManager.CreateAsync(Admin, "Admin01?");
      await uManager.AddToRoleAsync(Admin, "Admin");
    }
  }
}
