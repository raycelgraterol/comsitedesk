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

        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<ClientTypes> ClientTypes { get; set; }
        public DbSet<OrganizationTypes> OrganizationTypes { get; set; }
        public DbSet<TicketTypes> TicketTypes { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<TicketProcesses> TicketProcesses { get; set; }
        public DbSet<TicketCategories> TicketCategories { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<TicketsUsers> TicketsUsers { get; set; } 
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }

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

            builder.Entity<TicketsUsers>()
                .HasKey(x => new { x.TicketsId, x.UserId });
            builder.Entity<TicketsUsers>()
                .HasOne(x => x.Tickets)
                .WithMany(m => m.Users)
                .HasForeignKey(x => x.TicketsId);
            builder.Entity<TicketsUsers>()
                .HasOne(x => x.User)
                .WithMany(e => e.Tickets)
                .HasForeignKey(x => x.UserId);

            builder.Entity<TicketsEquipments>()
                .HasKey(x => new { x.TicketsId, x.EquipmentId });
            builder.Entity<TicketsEquipments>()
                .HasOne(x => x.Tickets)
                .WithMany(m => m.Equipments)
                .HasForeignKey(x => x.TicketsId);
            builder.Entity<TicketsEquipments>()
                .HasOne(x => x.Equipment)
                .WithMany(e => e.Tickets)
                .HasForeignKey(x => x.EquipmentId);


            #region All Roles
            builder.Entity<Role>()
                .HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "0b245219-a9b6-471c-84ac-60d7dc0c7bbd"
                    },
                    new Role
                    {
                        Id = 2,
                        Name = "Super Admin",
                        NormalizedName = "SUPER ADMIN",
                        ConcurrencyStamp = "56fd6ff4-2964-48ec-90d6-964dd677ed08"
                    },
                    new Role
                    {
                        Id = 3,
                        Name = "Presidente",
                        NormalizedName = "PRESIDENTE",
                        ConcurrencyStamp = "2289fbd7-586c-4c33-b03e-4aba7cb8dfb8"
                    },
                    new Role
                    {
                        Id = 4,
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = "bc346c93-7929-43e0-b168-f25e755074f1"
                    }
                );
            #endregion

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

            builder.Entity<OrganizationTypes>()
               .HasData(
                   new OrganizationTypes
                   {
                       Id = 1,
                       Name = "Organizacion Matriz"
                   },
                   new OrganizationTypes
                   {
                       Id = 2,
                       Name = "Organizacion Principal",
                   },
                   new OrganizationTypes
                   {
                       Id = 3,
                       Name = "Organizacion Secundaria",
                   }
               );

            builder.Entity<Organizations>()
               .HasData(
                   new Organizations
                   {
                       Id = 1,
                       BusinessName = "ComSite, C.A.",
                       RIF = "J-308373478",
                       Email = "daniel@comsite.com.ve",
                       PhoneNumber = "02126616922",
                       Address = "Av. La Colina Edf. Florencia Local 2 Los Chaguaramos Caracas – Venezuela",
                       OrganizationTypesId = 1,
                       ParentOrganizationId = null,
                       CreatedBy = 1,
                       DateCreated = Convert.ToDateTime("03/01/2021"),
                       IsActive = true
                   }
               );
        }        
    }
}
