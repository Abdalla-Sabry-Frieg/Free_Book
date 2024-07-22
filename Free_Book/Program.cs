using Domain.Entity;
using InfraStructure.Data;
using InfraStructure.IRepository;
using InfraStructure.IRepository.ServicesRepository;
using InfraStructure.Seeds;
using InfraStructure.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Free_Book
{
    public class Program 
    {
        public static async Task Main(string[] args)
        {
             var builder =  WebApplication.CreateBuilder(args);

            var host = CreateHostBuilder(args).Build();
            using var Scope = host.Services.CreateScope();
            var services = Scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultRole.SeedAsync(roleManager);
                await DefaultUser.SeedSuperAdminUserAsync(userManager, roleManager);
                await DefaultUser.SeedBasicUserAsync(userManager, roleManager);
            }
            catch (Exception) { throw; }

            host.Run();

            // Add services to the container.
          //  builder.Services.AddControllersWithViews();
            // builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection")));

            // Add IdentityRoles services

            // builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            // add sweet alert 

            //  builder.Services.AddSession();

            // Services to edit password

            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequireDigit= false;
            //    options.Password.RequireLowercase= false;
            //    options.Password.RequireUppercase= false;
            //    options.Password.RequireNonAlphanumeric= false;
            //    options.Password.RequiredUniqueChars = 0;
            //});

            //builder.Services.ConfigureApplicationCookie(option =>
            //{
            //    option.LoginPath = "/Admin";
            //    option.AccessDeniedPath = "/Admin/Home/Denied";
            //});


            //// Add IRepository Scope <T>

            //builder.Services.AddScoped<IServicesRepository<Category>,ServicesCategory>();
            //builder.Services.AddScoped<IServicesRepositoryLog<LogCategory> , ServicesLogCategory>();

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //// add sesions
            //app.UseSession();
            //// 1
            //app.UseAuthentication();
            //// 2
            //app.UseAuthorization();
            
          

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //      name: "areas",
            //      pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}"
            //    );
            //});

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");


         
            //app.Run();


            static IHostBuilder CreateHostBuilder(string[] args)
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
            }
        }
    }
}
