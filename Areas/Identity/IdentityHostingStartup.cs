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
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<webapplicationIdentityDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("webapplicationIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<webapplicationIdentityDbContext>();
                
                services.AddRazorPages();

                services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
            });
            
        }
    }
}