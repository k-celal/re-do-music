using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReDoMusic.Domain.Entites;
using ReDoMusic.Persistance.Contexts;
using RedoMusic.Persistence;
using re_do_music.MVC.TagHelpers;
using System.Security.Claims;

namespace re_do_music.MVC
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Hizmetleri ekleyin
            services.AddControllersWithViews();

            // Entity Framework ve Identity hizmetlerini ekleyin
            services.AddDbContext<ReDoMusicDbContext>(options =>
            {
                options.UseNpgsql(Configurations.GetString("ConnectionStrings:PostgreSQL"));
            });

            services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<ReDoMusicDbContext>()
              .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder();
                cookieBuilder.Name = "UdemyAppCookie";
                opt.LoginPath = new PathString("/home/signin");
                opt.LogoutPath = new PathString("/member/signout");
                opt.AccessDeniedPath = new PathString("/member/accessdenied");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);
                opt.SlidingExpiration = true;
            });
            // Diðer hizmetleri ekleyin
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireRole("Admin");
                });

                options.AddPolicy("UserPolicy", policy =>
                {
                    policy.RequireRole("User");
                });
            });

        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Instrument}/{action=Index}/{id?}");
            });
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);

            var app = builder.Build();
            Configure(app, builder.Environment);

            app.Run();
        }
    }
}
