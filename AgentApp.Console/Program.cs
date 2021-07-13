using System;
using System.IO;
using MediatR;
using Mediavalet.Domain;
using Mediavalet.Domain.Interfaces;
using Mediavalet.Domain.Services;
using MediaValet.Core.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgentApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Staring Agent App Instance");
            // Create service collection and configure our services
            var services = ConfigureServices();

            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<Agent>().Run().Wait();
        }


        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                             reloadOnChange: true);
            return builder.Build();
        }


        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            // Set up the objects we need to get to configuration settings
            var config = LoadConfiguration();
            // Add the config to our DI container for later user
            services.AddSingleton(config);

            services.Configure<AzureConfig>(config.GetSection(nameof(AzureConfig)));

            services.AddScoped<IAzureQueue, AzureQueueService>();
            services.AddScoped<IAzureTableStorage, AzureStorageTableService>();
            services.AddScoped<IOrderCounterService, OrderCounterService>();
            services.AddMediatR(typeof(MediatREntryPoint).Assembly);

            services.AddTransient<Agent>();

            return services;
        }

    }
}
