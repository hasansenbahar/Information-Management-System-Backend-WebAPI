using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;
using WebService.API.Data.Seeds;
using WebService.API.Data.Entity;
using Serilog;

namespace WebService.API.Data
{
    public class WebAPIDb : IdentityDbContext<User,Role,string>
    {
        public WebAPIDb()
        {
        }

        public WebAPIDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Allocation> Allocation { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WebAPIDb"]);


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigurationUserAndRole();


            //builder.HasDefaultSchema("Identity");

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<Role> (entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            //builder.Entity<Person>()
            //.HasMany(e => e.Allocations)
            //.WithOne(e => e.Person)
            //.HasForeignKey(e => e.PersonId)
            //.HasPrincipalKey(e => e.Id);


        }
    }

}
