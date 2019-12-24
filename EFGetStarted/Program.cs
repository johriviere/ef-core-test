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

            BuildWebHost(args).Services.GetService<Worker>().Run();
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

        private static UselessWrapper BuildWebHost(string[] args)
        {
            // create service collection
            var services = RegisterServices();

            // create service provider
            var serviceProvider = services.BuildServiceProvider();
            return new UselessWrapper { Services = serviceProvider };
        }

        private class UselessWrapper
        {
            public System.IServiceProvider Services { get; set; }
        }


    }
}
