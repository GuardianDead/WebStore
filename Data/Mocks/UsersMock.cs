using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore.Data.Entities;
using WebStore.Data.Identity;
using WebStore.Domain.Consts;

namespace WebStore.Data.Mocks
{
    public class UsersMock
    {
        public static void Init(AppDbContext db, UserManager<AppIdentityUser> userManager)
        {
            if (db.Users.FirstOrDefault() != null && userManager.FindByEmailAsync("kakawkawww13@mail.ru").GetAwaiter().GetResult() != null)
            {
                return;
            }

            var user = new AppIdentityUser("Александр", "Андианов", "Евгеньевич", db.Orders.Take(3).ToList(),
                new List<ProductArticle>(), db.ProductArticles.Take(3).ToList(), "kakawkawww13@mail.ru",
                "79157675803", "21081990wwwWWW");

            var result = userManager.CreateAsync(user, user.Password).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }

            result = userManager.AddToRoleAsync(user, RoleConst.Admin).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }

            var token = userManager.GenerateEmailConfirmationTokenAsync(user).GetAwaiter().GetResult();
            result = userManager.ConfirmEmailAsync(user, token).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }

            user.PhoneNumberConfirmed = true;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RoleConst.Admin),
                new Claim(ClaimTypes.GivenName,"GuardianDead"),
                new Claim(ClaimTypes.Email,"kakawkawww13@mail.ru"),
                new Claim(ClaimTypes.MobilePhone,"79157675803"),
                new Claim(ClaimTypes.DateOfBirth,"24.11.2002"),
                new Claim(ClaimTypes.Country,"Россия"),
                new Claim(ClaimTypes.Gender,"Мужской"),
            };
            result = userManager.AddClaimsAsync(user, claims).GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }
        }
        public static async Task InitAsync(AppDbContext db, UserManager<AppIdentityUser> userManager)
        {
            if (db.Users.FirstOrDefault() != null && await userManager.FindByEmailAsync("kakawkawww13@mail.ru") != null)
            {
                return;
            }

            var user = new AppIdentityUser("Александр", "Андианов", "Евгеньевич", await db.Orders.Take(3).ToListAsync(),
                new List<ProductArticle>(), await db.ProductArticles.Take(3).ToListAsync(), "kakawkawww13@mail.ru",
                "79157675803", "21081990wwwWWW");

            var result = await userManager.CreateAsync(user, user.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }

            result = await userManager.AddToRoleAsync(user, RoleConst.Admin);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            result = await userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.GivenName,"GuardianDead"),
                new Claim(ClaimTypes.Email,"kakawkawww13@mail.ru"),
                new Claim(ClaimTypes.MobilePhone,"79157675803"),
                new Claim(ClaimTypes.DateOfBirth,"24.11.2002"),
                new Claim(ClaimTypes.Country,"Россия"),
                new Claim(ClaimTypes.Gender,"Мужской"),
            };
            result = await userManager.AddClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                throw new Exception();
            }
        }
    }
}
