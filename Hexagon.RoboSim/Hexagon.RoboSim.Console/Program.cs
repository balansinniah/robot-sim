using Hexagon.RoboSim.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Hexagon.RoboSim.Console
{
    class Program
    {
        //public static ILoggerFactory LoggerFactory;
        //public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            var app             = serviceProvider.GetService<MainApp>();

            app.Run();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            IConfigurationRoot configuration = GetConfiguration();
            services.AddSingleton<IConfigurationRoot>(configuration);

            // Support typed Options
            services.AddOptions();
            services.Configure<MoveArea>(configuration.GetSection("MoveArea"));
            services.Configure<RoboCommandSource>(configuration.GetSection("RoboCommandSource"));

            services.AddTransient<MainApp>();
        }
        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }

    }
}
