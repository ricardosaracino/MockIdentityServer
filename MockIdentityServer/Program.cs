using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Logging;
using Serilog;

namespace MockIdentityServer
{
    /// <summary>
    /// https://github.com/scottbrady91/IdentityServer4-Example
    /// </summary>
    public class Program
    {
        public static int Main(string[] args)
        {
            IdentityModelEventSource.ShowPII = true;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
            
            try
            {
                Log.Information("Starting host...");
                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:4000")
                .UseStartup<Startup>();
    }
}