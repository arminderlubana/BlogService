using BlogService.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BlogService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                   
                  //  var context = services.GetRequiredService<SchoolContext>();
                    // if(context.Database.EnsureCreated()){
                    //     RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                    //  databaseCreator.CreateTables();

                    // }
                  //  DbInitializer.EnsureMigration<SchoolContext>(context);
                  //  DbInitializer.InitializeSchoolContext<SchoolContext>(context);
                    var econtext = services.GetRequiredService<EmployeeContext>();
                       // econtext.Database.EnsureCreated();

                // if(econtext.Database.EnsureCreated()){
                    //     RelationalDatabaseCreator edatabasecreator = (RelationalDatabaseCreator)econtext.Database.GetService<IDatabaseCreator>();
                    //  edatabasecreator.CreateTables();
                 //  }
                    DbInitializer.EnsureMigration<EmployeeContext>(econtext);
                    DbInitializer.InitializeEmployeeContext<EmployeeContext>(econtext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
             })
                .Build();
    }
}
