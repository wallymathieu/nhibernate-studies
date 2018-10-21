using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SomeBasicNHApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, conf) =>
                {
                    //.SetBasePath(env.ContentRootPath)
                    conf.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.user.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                    if (args != null)
                    {
                        conf.AddCommandLine(args);
                    }
                })
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .CaptureStartupErrors(true)
                .Build();
    }
}