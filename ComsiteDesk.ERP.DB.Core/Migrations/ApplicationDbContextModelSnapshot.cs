﻿// <auto-generated />
using System;
using ComsiteDesk.ERP.DB.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComsiteDesk.ERP.DB.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Authentication.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("OrganizationId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.ClientTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ClientTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "N",
                            Name = "Natural"
                        },
                        new
                        {
                            Id = 2,
                            Code = "J",
                            Name = "Juridicada"
                        });
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("Serial")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("features")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.OrganizationTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("OrganizationTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Organizacion Matriz"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Organizacion Principal"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Organizacion Secundaria"
                        });
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.Organizations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationTypesId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentOrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RIF")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("OrganizationTypesId");

                    b.HasIndex("ParentOrganizationId");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Av. La Colina Edf. Florencia Local 2 Los Chaguaramos Caracas – Venezuela",
                            BusinessName = "ComSite, C.A.",
                            CreatedBy = 1,
                            DateCreated = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "daniel@comsite.com.ve",
                            IsActive = true,
                            OrganizationTypesId = 1,
                            PhoneNumber = "02126616922",
                            RIF = "J-308373478"
                        });
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("TicketCategories");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketProcesses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Step")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TicketProcesses");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("TicketStatus");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.Tickets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("HoursWorked")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<string>("ReportedFailure")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("SolutionDone")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TechnicalFailure")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("TicketCategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TicketDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TicketProcessId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("TicketStatusId")
                        .HasColumnType("int");

                    b.Property<int>("TicketTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("TicketCategoryId");

                    b.HasIndex("TicketProcessId");

                    b.HasIndex("TicketStatusId");

                    b.HasIndex("TicketTypeId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketsEquipments", b =>
                {
                    b.Property<int>("TicketsId")
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.HasKey("TicketsId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("TicketsEquipments");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketsUsers", b =>
                {
                    b.Property<int>("TicketsId")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("TicketsId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TicketsUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<long>", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole<long>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Authentication.Role", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole<long>");

                    b.HasDiscriminator().HasValue("Role");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ConcurrencyStamp = "0b245219-a9b6-471c-84ac-60d7dc0c7bbd",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2L,
                            ConcurrencyStamp = "56fd6ff4-2964-48ec-90d6-964dd677ed08",
                            Name = "Super Admin",
                            NormalizedName = "SUPER ADMIN"
                        },
                        new
                        {
                            Id = 3L,
                            ConcurrencyStamp = "2289fbd7-586c-4c33-b03e-4aba7cb8dfb8",
                            Name = "Presidente",
                            NormalizedName = "PRESIDENTE"
                        },
                        new
                        {
                            Id = 4L,
                            ConcurrencyStamp = "bc346c93-7929-43e0-b168-f25e755074f1",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Authentication.User", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.Equipment", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.Organizations", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.OrganizationTypes", "OrganizationTypes")
                        .WithMany()
                        .HasForeignKey("OrganizationTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Organizations", "ParentOrganization")
                        .WithMany()
                        .HasForeignKey("ParentOrganizationId");
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.Tickets", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Organizations", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.TicketCategories", "TicketCategory")
                        .WithMany()
                        .HasForeignKey("TicketCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.TicketProcesses", "TicketProcess")
                        .WithMany()
                        .HasForeignKey("TicketProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.TicketStatus", "TicketStatus")
                        .WithMany()
                        .HasForeignKey("TicketStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.TicketTypes", "TicketType")
                        .WithMany()
                        .HasForeignKey("TicketTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketsEquipments", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Equipment", "Equipment")
                        .WithMany("Tickets")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Tickets", "Tickets")
                        .WithMany("Equipments")
                        .HasForeignKey("TicketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ComsiteDesk.ERP.DB.Core.Models.TicketsUsers", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Models.Tickets", "Tickets")
                        .WithMany("Users")
                        .HasForeignKey("TicketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Authentication.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<long>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<long>", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Authentication.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<long>", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Authentication.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<long>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<long>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ComsiteDesk.ERP.DB.Core.Authentication.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<long>", b =>
                {
                    b.HasOne("ComsiteDesk.ERP.DB.Core.Authentication.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
