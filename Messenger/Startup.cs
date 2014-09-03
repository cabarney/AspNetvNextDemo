using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.OptionsModel;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Authentication;
using Microsoft.AspNet.Security.Cookies;

using Messenger.Models;

namespace Messenger
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("localconfig.json");

            // Custom Middleware
            //app.UseMiddleware<LoggingMiddleware>();

            app.UseStaticFiles();
            app.UseErrorPage();


            app.UseServices(services =>
            {
                services.AddEntityFramework().AddSqlServer();

                services.AddScoped<MessengerDataContext>();


                services.SetupOptions<MessengerDbContextOptions>(options =>
                    {
                        options.UseSqlServer(configuration.Get("Data:DefaultConnection:ConnectionString"));
                    });

                // Identity
                /*
                services.AddIdentitySqlServer<MessengerDataContext, ApplicationUser>()
                    .AddAuthentication()
                    .SetupOptions(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonLetterOrDigit = false;
                    });
                */

                services.AddMvc();
            });

            // Identity
            /*
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = ClaimsIdentityOptions.DefaultAuthenticationType,
                LoginPath = new PathString("/Account/Login"),
                Notifications = new CookieAuthenticationNotifications
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(0))
                }
            });

            app.UseTwoFactorSignInCookies();
            */

            app.UseMvc();

            // Custom Routing
            /*
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Messages}/{action=Index}/{id?}");
                });
            */
            app.UseWelcomePage();
        }       
    }
}
