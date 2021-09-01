using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.DB.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using dbCore = ComsiteDesk.ERP.DB.Core.Models;

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

        public DbSet<Module> Modules { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<dbCore.Action> Actions { get; set; }
        public DbSet<RoleFormAction> RoleFormAction { get; set; }

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

            builder.Entity<FormAction>()
                .HasIndex(x => new { x.FormId, x.ActionId })
                .IsUnique();

            builder.Entity<FormAction>()
                .HasOne(x => x.Form)
                .WithMany(m => m.Actions)
                .HasForeignKey(x => x.FormId);
            builder.Entity<FormAction>()
                .HasOne(x => x.Action)
                .WithMany(e => e.Forms)
                .HasForeignKey(x => x.ActionId);

            builder.Entity<RoleFormAction>()
                .HasKey(x => new { x.RoleId, x.FormActionId });
            builder.Entity<RoleFormAction>()
                .HasOne(x => x.Role)
                .WithMany(m => m.FormActions)
                .HasForeignKey(x => x.RoleId);
            builder.Entity<RoleFormAction>()
                .HasOne(x => x.FormAction)
                .WithMany(e => e.Roles)
                .HasForeignKey(x => x.FormActionId);


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
                    },
                    new Role
                    {
                        Id = 5,
                        Name = "Tecnico Soporte I",
                        NormalizedName = "TECNICO SOPORTE I",
                        ConcurrencyStamp = "78c9b629-f045-47cf-954c-17e5907dac94"
                    },
                    new Role
                    {
                        Id = 6,
                        Name = "Tecnico Soporte II",
                        NormalizedName = "TECNICO SOPORTE II",
                        ConcurrencyStamp = "92f79ac3-ad69-4425-bca5-d91d330d74fc"
                    },
                    new Role
                    {
                        Id = 7,
                        Name = "Tecnico Soporte III",
                        NormalizedName = "TECNICO SOPORTE III",
                        ConcurrencyStamp = "77c80bfc-a386-4633-8a04-bf0e539dc2c7"
                    },
                    new Role
                    {
                        Id = 8,
                        Name = "Gerente Soporte",
                        NormalizedName = "GERENTE SOPORTE",
                        ConcurrencyStamp = "ab6bea7a-64a7-4d88-a85d-8eb490d57cc9"
                    },
                    new Role
                    {
                        Id = 9,
                        Name = "Asistente Administrativo",
                        NormalizedName = "ASISTENTE ADMINISTRATIVO",
                        ConcurrencyStamp = "a80892a0-fb99-4928-b6b6-86aae2fe2ffc"
                    },
                    new Role
                    {
                        Id = 10,
                        Name = "Vendedor",
                        NormalizedName = "VENDEDOR",
                        ConcurrencyStamp = "d96eaba3-0ea0-4521-bb98-1bdad3a31024"
                    },
                    new Role
                    {
                        Id = 11,
                        Name = "Finanzas",
                        NormalizedName = "FINANZAS",
                        ConcurrencyStamp = "fd734eb1-d558-4d89-a238-547f83e5df0a"
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

            builder.Entity<TaskStatus>()
              .HasData(
                  new TaskStatus
                  {
                      Id = 1,
                      Name = "Por Hacer",
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021"),
                      IsActive = true
                  },
                  new TaskStatus
                  {
                      Id = 2,
                      Name = "En proceso",
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021"),
                      IsActive = true
                  },
                  new TaskStatus
                  {
                      Id = 3,
                      Name = "Terminada",
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021"),
                      IsActive = true
                  }
              );

            #region Segurity - Modules

            builder.Entity<Module>()
              .HasData(
                  new Module
                  {
                      Id = 1,
                      Name = "Seguridad",
                      Description = "Seguridad",
                      URI = "/security",
                      IsActive = true,
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021")
                  },
                  new Module
                  {
                      Id = 2,
                      Name = "Configuracion",
                      Description = "Configuracion",
                      URI = "/configuration",
                      IsActive = true,
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021")
                  },
                  new Module
                  {
                      Id = 3,
                      Name = "Proyectos",
                      Description = "Proyectos",
                      URI = "/projects",
                      IsActive = true,
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021")
                  },
                  new Module
                  {
                      Id = 4,
                      Name = "Tickets",
                      Description = "Tickets",
                      URI = "/tickets-management",
                      IsActive = true,
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021")
                  },
                  new Module
                  {
                      Id = 5,
                      Name = "Tareas",
                      Description = "Tareas",
                      URI = "/assignment",
                      IsActive = true,
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01/01/2021")
                  },
                  new Module
                  {
                      Id = 6,
                      Name = "Dashboard",
                      Description = "Dashboard",
                      URI = "/",
                      CreatedBy = 1,
                      DateCreated = Convert.ToDateTime("01-01-2021"),
                      IsActive = true
                  }

              );

            #endregion

            #region Segurity - Actions
            builder.Entity<dbCore.Action>()
               .HasData(
                   new dbCore.Action
                   {
                       Id = 1,
                       Name = "Visualizar",
                       Description = "Visualizar",
                       IsActive = true,
                       DateCreated = Convert.ToDateTime("01-01-2021"),
                       CreatedBy = 1
                   },
                   new dbCore.Action
                   {
                       Id = 2,
                       Name = "Listar",
                       Description = "Listar",
                       IsActive = true,
                       DateCreated = Convert.ToDateTime("01-01-2021"),
                       CreatedBy = 1
                   },
                   new dbCore.Action
                   {
                       Id = 3,
                       Name = "Crear",
                       Description = "Crear",
                       IsActive = true,
                       DateCreated = Convert.ToDateTime("01-01-2021"),
                       CreatedBy = 1
                   },
                   new dbCore.Action
                   {
                       Id = 4,
                       Name = "Modificar",
                       Description = "Modificar",
                       IsActive = true,
                       DateCreated = Convert.ToDateTime("01-01-2021"),
                       CreatedBy = 1
                   },
                   new dbCore.Action
                   {
                       Id = 5,
                       Name = "Eliminar",
                       Description = "Eliminar",
                       IsActive = true,
                       DateCreated = Convert.ToDateTime("01-01-2021"),
                       CreatedBy = 1
                   },
                   new dbCore.Action
                   {
                       Id = 6,
                       Name = "Activar",
                       Description = "Activar",
                       IsActive = true,
                       DateCreated = Convert.ToDateTime("01-01-2021"),
                       CreatedBy = 1
                   }
               );
            #endregion

            #region Segurity - Views
            builder.Entity<Form>()
                .HasData(
                    new Form
                    {
                        Id = 1,
                        Name = "Usuarios",
                        Description = "Usuarios",
                        URI = "/users",
                        ModuleId = 1,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 2,
                        Name = "Roles",
                        Description = "Roles",
                        URI = "/roles",
                        ModuleId = 1,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },//SECURITY
                    new Form
                    {
                        Id = 3,
                        Name = "Sedes",
                        Description = "Sedes",
                        URI = "/headquarter",
                        ModuleId = 2,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 4,
                        Name = "Departamentos",
                        Description = "Departamentos",
                        URI = "/department",
                        ModuleId = 2,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 5,
                        Name = "Equipos",
                        Description = "Equipos",
                        URI = "/equipment",
                        ModuleId = 2,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 6,
                        Name = "Usuarios de Equipos",
                        Description = "Usuarios de Equipos",
                        URI = "/equipmentUser",
                        ModuleId = 2,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },//PROJECTS
                    new Form
                    {
                        Id = 7,
                        Name = "Proyectos",
                        Description = "Proyectos",
                        URI = "/list",
                        ModuleId = 3,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 8,
                        Name = "Estatus de Proyectos",
                        Description = "Estatus de Proyectos",
                        URI = "/status",
                        ModuleId = 3,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },//TICKETS
                    new Form
                    {
                        Id = 9,
                        Name = "Tickets",
                        Description = "Tickets",
                        URI = "/tickets",
                        ModuleId = 4,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 10,
                        Name = "Tipos de tickets",
                        Description = "Tipos de tickets",
                        URI = "/types",
                        ModuleId = 4,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 11,
                        Name = "Categorias de tickets",
                        Description = "Categorias de tickets",
                        URI = "/categories",
                        ModuleId = 4,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 12,
                        Name = "Procesos de tickets",
                        Description = "Procesos de tickets",
                        URI = "/processes",
                        ModuleId = 4,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 13,
                        Name = "Estatus de tickets",
                        Description = "Estatus de tickets",
                        URI = "/status",
                        ModuleId = 4,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },//Assignment
                    new Form
                    {
                        Id = 14,
                        Name = "Lista de tareas",
                        Description = "Lista de tareas",
                        URI = "/list",
                        ModuleId = 5,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },
                    new Form
                    {
                        Id = 15,
                        Name = "Estatus de tareas",
                        Description = "Estatus de tareas",
                        URI = "/status",
                        ModuleId = 5,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    },//DashBoard
                    new Form
                    {
                        Id = 16,
                        Name = "Panel principal",
                        Description = "Panel principal",
                        URI = "/dashboard",
                        ModuleId = 6,
                        DateCreated = Convert.ToDateTime("01-01-2021"),
                        CreatedBy = 1,
                        IsActive = true
                    }

                );
            #endregion
        }
    }
}
