using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Claims;
using WebService.API.Authorization;
using WebService.API.Data.Entity;
using WebService.API.Helpers;
using WebService.API.Models.AuthModels;

namespace WebService.API.Data.Seeds
{
    public static class Seeds
    {
        public static void ConfigurationUserAndRole(this ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
            new Role
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fduu10",
                Name = Roles.SuperAdmin.ToString(),
                NormalizedName = NormalizedRoles.SUPERADMIN.ToString(),
            },
             new Role
             {
                 Id = "2c5as74e-sdfr-446f-86af-483d56fhh210",
                 Name = Roles.Admin.ToString(),
                 NormalizedName = NormalizedRoles.ADMIN.ToString(),
             },
             new Role
             {
                 Id = "2c5e1dde-3b0e-45d3-86af-483d56fll210",
                 Name = Roles.Basic.ToString(),
                 NormalizedName = NormalizedRoles.BASIC.ToString(),
             }
        );
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(

                new User
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "admin",
                    Email = "admin@mail.com",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@MAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "admin"),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = "8e4458as-a24d-3456-a6c6-944fhj48cdb9",
                    UserName = "superadmin",
                    Email = "superadmin@mail.com",
                    NormalizedUserName = "SUPERADMIN",
                    NormalizedEmail = "SUPERADMIN@MAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "superadmin"),
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                 new User
                 {
                     Id = "123458as-a24d-3456-a6c6-123fhj48cdb9",
                     UserName = "basic",
                     Email = "basic@mail.com",
                     NormalizedUserName = "BASIC",
                     NormalizedEmail = "BASIC@MAIL.COM",
                     PasswordHash = hasher.HashPassword(null, "basic"),
                     EmailConfirmed = true,
                     LockoutEnabled = true,
                     PhoneNumberConfirmed = true,
                     SecurityStamp = Guid.NewGuid().ToString()
                 }
            );


            builder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>()
               {
                   RoleId = "2c5e174e-3b0e-446f-86af-483d56fduu10", // super admin role
                   UserId = "8e4458as-a24d-3456-a6c6-944fhj48cdb9" // super admin user
               },
                new IdentityUserRole<string>()
                {
                    RoleId = "2c5as74e-sdfr-446f-86af-483d56fhh210", // admin role
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9" // admin user
                },
                  new IdentityUserRole<string>()
                  {
                      RoleId = "2c5e1dde-3b0e-45d3-86af-483d56fll210", // basic role
                      UserId = "123458as-a24d-3456-a6c6-123fhj48cdb9" // basic user
                  }
               );
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions), "2c5e174e-3b0e-446f-86af-483d56fduu10");

            var i = 1;

            foreach (var permission in allPermissions)
            {

                builder.Entity<IdentityRoleClaim<string>>().HasData(
                   new IdentityRoleClaim<string>()
                   {
                       Id = i,
                       RoleId = "2c5e174e-3b0e-446f-86af-483d56fduu10",
                       ClaimType = "Permission",
                       ClaimValue = permission.Value
                   }
                   );
                i++;
            }
        }
    }
}
