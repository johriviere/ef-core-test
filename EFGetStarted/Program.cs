using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace EFGetStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. 
            var services = RegisterServices();

            // 2.
            var serviceProvider = services.BuildServiceProvider();

            // 3.
            var worker = serviceProvider.GetService<Worker>();
            worker.Run();
        }
          
        private static IServiceCollection RegisterServices()
        {
            var services = new ServiceCollection();

            // config
            var config = LoadConfiguration();
            services.AddSingleton(config);

            // ef core
            services.AddDbContextPool<BloggingContext>(opt => opt.UseSqlite(config.GetConnectionString("BloggingConnection")));

            // business classes
            services.AddTransient<Worker>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false,
                             reloadOnChange: true);
            return builder.Build();
        }
    }
}
