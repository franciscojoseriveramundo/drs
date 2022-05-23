using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DRS.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var serviceCollection = new ServiceCollection();
            //serviceCollection.AddDataProtection()
            //    .SetApplicationName("DRS")
            //    .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"~\"))
            //    .SetDefaultKeyLifetime(TimeSpan.FromDays(36500));

            //var services = serviceCollection.BuildServiceProvider();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
