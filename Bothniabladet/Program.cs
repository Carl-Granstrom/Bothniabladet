using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bothniabladet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
        new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureAppConfiguration(AddAppConfiguration)
        .ConfigureLogging(
        (hostingContext, logging) => { /* Detail not shown */ })
        .UseIISIntegration()
        .UseDefaultServiceProvider(
        (context, options) => { /* Detail not shown */ })
        .UseStartup<Startup>()
        .Build();
        public static void AddAppConfiguration(
        WebHostBuilderContext hostingContext,
        IConfigurationBuilder config)
        {
            config.AddJsonFile("appsettings.json", optional: true);
            config.AddUserSecrets<Startup>();   //I'm not sure what I'm doing here, this might be all wrong!!!
            
        }
    }
}
