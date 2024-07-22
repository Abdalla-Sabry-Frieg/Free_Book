using Domain.Constants;
using Domain.Entity;
using InfraStructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entity.Helper;

namespace InfraStructure.Seeds
{
    public static class DefaultUser
    {
        public static async Task SeedSuperAdminUserAsync(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = Helper.UserName,
                Email = Helper.Email,
                Name = Helper.Name,
                ImageUser = "Defult.png",
                ActiveUser = true,
                EmailConfirmed = true,
            };

            var user = await userManager.FindByEmailAsync(DefaultUser.Email); 

            if(user == null) 
            {
                await userManager.CreateAsync(DefaultUser , Helper.Password);
                await userManager.AddToRolesAsync(DefaultUser, new List<string> { Helper.Roles.SuperAdmin.ToString()});
            }

            await roleManager.SeedClaimsAsync();
        }

        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = Helper.UserNameBasic,
                Email = Helper.EmailBasic,
                Name = Helper.NameBasic,
                ImageUser = "Defult.png",
                ActiveUser = true,
                EmailConfirmed = true,
            };

            var user = userManager.FindByEmailAsync(DefaultUser.Email);

            if (user.Result == null)
            {
                await userManager.CreateAsync(DefaultUser, Helper.PasswordBasic);
                await userManager.AddToRolesAsync(DefaultUser, new List<string> { Helper.Roles.Basic.ToString() });
            }

        }

        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());
            //Code to Add permission Claims

            var modules = Enum.GetValues(typeof(PermissionModuleName));

            foreach (var module in modules)
                await roleManager.AddPermissionClaims(adminRole, module.ToString());
        }

        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsFromModule(module);

            foreach (var permission in allPermissions)
                if (!allClaims.Any(x => x.Type == Permission && x.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim(Permission, permission)); // Type and Value

        }

    }
}
