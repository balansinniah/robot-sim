using Hexagon.RoboSim.Services;
using Hexagon.RoboSim.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hexagon.RoboSim.Console.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            // add services
            services.AddTransient<IMovementService<int,int>, MovementService<int, int>>();

            // add app
            services.AddTransient<MainApp>();
        }
    }
}
