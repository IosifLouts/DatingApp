using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args) //what goes inside here, happens befroe our application gets started.
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope(); //we need to get our DataContext service in order to parse it in our Seed method
            var services = scope.ServiceProvider; //create a scope for the services that we are gonna create in this part.

            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync(); //Convenience. Restart our app to apply any migrations instead of
                await Seed.SeedUsers(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex,"An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
