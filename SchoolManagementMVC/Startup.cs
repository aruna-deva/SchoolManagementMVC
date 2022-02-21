using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolManagementMVC.Models.Infrastructure;
using SchoolManagementSystem.Infrastructure;
using SchoolManagementSystem.Models;

namespace SchoolManagementMVC
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
            services.AddScoped<sendServiceMessage>();
            services.AddControllersWithViews();
            services.AddDbContext<SchoolManagementSystem.Infrastructure.SchoolManagementDbContext>
                (
                options => options.UseSqlServer(Configuration.GetConnectionString("DbCon"))
                );
            services.AddScoped<ICRUDRepository<ClassRoom, int>, ClassRoomRepository>();
            services.AddScoped<ICRUDRepository<Staffclassification, int>, StaffClassificationRepository>();
            services.AddScoped<ICRUDRepository<StudentDetail, int>, StudentDetailRepository>();
            services.AddScoped<ICRUDRepository<TeachingStaffDetail, int>, TeachingStaffDetailRepository>();
            services.AddScoped<ICRUDRepository<TimeTable, int>, TimeTableRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    options.SlidingExpiration = true;
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
