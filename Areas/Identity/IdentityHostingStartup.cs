using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

[assembly: HostingStartup(typeof(webapplication.Areas.Identity.IdentityHostingStartup))]
namespace webapplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<GebruikerDbContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("Default")));

                services.AddIdentity<Gebruiker, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<GebruikerDbContext>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI();

                services.AddRazorPages();

                services.AddAuthorization(options =>
                {
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.AddPolicy("readpolicy",
                                builder => builder.RequireRole("Admin", "Hulpverlener", "Client", "Moderator"));
                    options.AddPolicy("writepolicy",
                        builder => builder.RequireRole("Admin"));
                });
            });

        }
    }
}
