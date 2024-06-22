using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.Domain.Contracts.Dtos.Auth;
using PayrollSystem.Infrastructure.SQL.SeedData.SeedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.SQL.SeedData
{
    public class SeedRolesAndClaims
    {
        public static async Task AdminSeeds(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var r = roleManager.Roles.ToList();
            var userAdmin = await userManager.FindByNameAsync("admin");
            if (userAdmin == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    isActive = true,
                };

                var result = await userManager.CreateAsync(user, "123Qwe1@@");
            }

            if (userAdmin != null)
            {
                foreach (var item in r)
                {
                    await userManager.AddToRoleAsync(userAdmin, item.Name);
                }
            }

        }
            public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Seed roles
            string[][] roles = { [Roles.Admin,Roles.FAdmin], [Roles.User,Roles.FUser], [Roles.Personnel,Roles.FPersonnel], [Roles.PaySlip,Roles.FPaySlip], [Roles.PayStatement,Roles.FPayStatement], [Roles.Setting,Roles.FSetting] };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role[0]))
                {
                    var NewRole = new ApplicationRole();
                    NewRole.Name = role[0];
                    NewRole.FA_name = role[1];
                    await roleManager.CreateAsync(NewRole);
                }
            }

            var adminRole = await roleManager.FindByNameAsync(Roles.Admin);
            var allClaims = new List<Claim>
        {
            new Claim($"Permission{adminRole}", Claims.Create),
            new Claim($"Permission{adminRole}", Claims.Read),
            new Claim($"Permission{adminRole}", Claims.Update),
            new Claim($"Permission{adminRole}", Claims.Delete)
        };

            foreach (var claim in allClaims)
            {
                if (!(await roleManager.GetClaimsAsync(adminRole)).Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(adminRole, claim);
                }
            }

            var settingRole = await roleManager.FindByNameAsync(Roles.Setting);
            var settingClaims = new List<Claim>
        {
            new Claim($"Permission{settingRole}", Claims.Create),
            new Claim($"Permission{settingRole}", Claims.Read),
            new Claim($"Permission{settingRole}", Claims.Update),
            new Claim($"Permission{settingRole}", Claims.Delete)
        };

            foreach (var claim in settingClaims)
            {
                if (!(await roleManager.GetClaimsAsync(settingRole)).Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(settingRole, claim);
                }
            }

            // Seed claims for User role
            var userRole = await roleManager.FindByNameAsync(Roles.User);
            var userClaims = new List<Claim>
        {
            new Claim($"Permission{userRole}", Claims.Create),
            new Claim($"Permission{userRole}", Claims.Read),
            new Claim($"Permission{userRole}", Claims.Update),
            new Claim($"Permission{userRole}", Claims.Delete)
        };

            foreach (var claim in userClaims)
            {
                if (!(await roleManager.GetClaimsAsync(userRole)).Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(userRole, claim);
                }
            }

            // Seed claims for Personnel role
            var personnelRole = await roleManager.FindByNameAsync(Roles.Personnel);
            var personnelClaims = new List<Claim>
        {
            new Claim($"Permission{personnelRole}", Claims.Read)
        };

            foreach (var claim in personnelClaims)
            {
                if (!(await roleManager.GetClaimsAsync(personnelRole)).Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(personnelRole, claim);
                }
            }

            // Seed claims for PaySlip role
            var paySlipRole = await roleManager.FindByNameAsync(Roles.PaySlip);
            var paySlipClaims = new List<Claim>
        {
            new Claim($"Permission{paySlipRole}", Claims.Read)
        };

            foreach (var claim in paySlipClaims)
            {
                if (!(await roleManager.GetClaimsAsync(paySlipRole)).Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(paySlipRole, claim);
                }
            }

            // Seed claims for PayStatement role
            var payStatementRole = await roleManager.FindByNameAsync(Roles.PayStatement);
            var payStatementClaims = new List<Claim>
        {
            new Claim("Permission", Claims.Read)
        };

            foreach (var claim in payStatementClaims)
            {
                if (!(await roleManager.GetClaimsAsync(payStatementRole)).Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(payStatementRole, claim);
                }
            }
        }
    }
}
