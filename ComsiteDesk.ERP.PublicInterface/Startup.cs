using ComsiteDesk.ERP.Data;
using ComsiteDesk.ERP.DB.Core;
using ComsiteDesk.ERP.DB.Core.Authentication;
using ComsiteDesk.ERP.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComsiteDesk.ERP.PublicInterface
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // For Entity Framework
            services.AddDbContext<ApplicationDbContext>(
                    options => options
                    .UseSqlServer(Configuration.GetConnectionString("ConnStrProd"))
                    .EnableSensitiveDataLogging()
                    );

            // For Identity
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes((24 * 60));//You can set Time   
            });

            // Inversion of Control(DI)
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IOrganizationsService, OrganizationsService>();
            services.AddTransient<IOrganizationsRepo, OrganizationsRepo>();

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IEquipmentRepo, EquipmentRepo>();

            services.AddTransient<ITicketCategoriesService, TicketCategoriesService>();
            services.AddTransient<ITicketCategoriesRepo, TicketCategoriesRepo>();

            services.AddTransient<ITicketProcessesService, TicketProcessesService>();
            services.AddTransient<ITicketProcessesRepo, TicketProcessesRepo>();

            services.AddTransient<ITicketsService, TicketsService>();
            services.AddTransient<ITicketsRepo, TicketsRepo>();

            services.AddTransient<ITicketStatusService, TicketStatusService>();
            services.AddTransient<ITicketStatusRepo, TicketStatusRepo>();

            services.AddTransient<ITicketTypesService, TicketTypesService>();
            services.AddTransient<ITicketTypesRepo, TicketTypesRepo>();

            services.AddTransient<IProjectsService, ProjectsService>();
            services.AddTransient<IProjectsRepo, ProjectsRepo>();

            services.AddTransient<IProjectStatusService, ProjectStatusService>();
            services.AddTransient<IProjectStatusRepo, ProjectStatusRepo>();

            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ITasksRepo, TasksRepo>();

            services.AddTransient<ITaskStatusService, TaskStatusService>();
            services.AddTransient<ITaskStatusRepo, TaskStatusRepo>();

            // configure jwt authentication
            var key = Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
