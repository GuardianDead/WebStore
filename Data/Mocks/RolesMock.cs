using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebStore.Data.Identity;

namespace WebStore.Data.Mocks
{
    public class RolesMock
    {
        public static void Init(RoleManager<AppIdentityRole> roleManager)
        {
            if(roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult() == false)
            {
                CreateRole("Admin", roleManager);
            }
            if (roleManager.RoleExistsAsync("Moderator").GetAwaiter().GetResult() == false)
            {
                CreateRole("Moderator", roleManager);
            }
            if (roleManager.RoleExistsAsync("User").GetAwaiter().GetResult() == false)
            {
                CreateRole("User", roleManager);
            }
            if (roleManager.RoleExistsAsync("Guest").GetAwaiter().GetResult() == false)
            {
                CreateRole("Guest", roleManager);
            }
        }
        public static async Task InitAsync(RoleManager<AppIdentityRole> roleManager)
        {
            if (await roleManager.RoleExistsAsync("Admin") == false)
            {
                await CreateRoleAsync("Admin", roleManager);
            }
            if (await roleManager.RoleExistsAsync("Moderator") == false)
            {
                await CreateRoleAsync("Moderator", roleManager);
            }
            if (await roleManager.RoleExistsAsync("User") == false)
            {
                await CreateRoleAsync("User", roleManager);
            }
            if (await roleManager.RoleExistsAsync("Guest") == false)
            {
                await CreateRoleAsync("Guest", roleManager);
            }
        }

        private static IdentityResult CreateRole(string name, RoleManager<AppIdentityRole> roleManager)
        {
            var result = roleManager.CreateAsync(new AppIdentityRole(name)).GetAwaiter().GetResult();
            if (!result.Succeeded)
{
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                    throw new Exception();
                }
            }
            return result;
        }
        private static async Task<IdentityResult> CreateRoleAsync(string name, RoleManager<AppIdentityRole> roleManager)
        {
            var result = await roleManager.CreateAsync(new AppIdentityRole(name));
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                    throw new Exception();
                }
            }
            return result;
        }
    }
}
