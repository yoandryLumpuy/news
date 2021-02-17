using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using news_api.Core.Model;
using news_api.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using news_api.Extensions;

namespace news_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();
            using (var serviceScope = hostBuilder.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;
                try
                {
                    var dbContext = serviceProvider.GetRequiredService<NewsDbContext>();
                    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                    dbContext.Database.Migrate();
                    ExtensionMethods.SeedUsers(userManager, roleManager, dbContext);
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.Log(LogLevel.Error, $"An error has occured during migration!. - {ex.Message}" );
                }
            }
            hostBuilder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
