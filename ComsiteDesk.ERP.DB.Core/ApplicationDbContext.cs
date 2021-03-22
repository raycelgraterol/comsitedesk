using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.DB.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComsiteDesk.ERP.DB.Core
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<long>, long, IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //Rename the ASP.NET Identity models
            builder.Entity<User>().ToTable("User");
            builder.Entity<IdentityRole<long>>().ToTable("Role");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaim");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogin");
            builder.Entity<IdentityUserRole<long>>().ToTable("UserRole");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserToken");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaim");

            builder.Entity<ClientTypes>()
               .HasData(
                   new ClientTypes
                   {
                       Id = 1,
                       Name = "Natural",
                       Code = "N"
                   },
                   new ClientTypes
                   {
                       Id = 2,
                       Name = "Juridicada",
                       Code = "J"
                   }
               );
        }

        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<ClientTypes> ClientTypes { get; set; }        
    }
}
