using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeseriesDatabase;

namespace SampleDataConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var scope = CreateServiceProvider().CreateScope())
            {
                var service = scope.ServiceProvider.GetService<ICreateSampleData>();
                await service.GenerateSampleData();
                Console.WriteLine("Finished!");
            }
            
        }
        private static void ConfigureServices(IServiceCollection isc)
        {
            isc.AddDbContext<TimeSeriesDbContext>();
            isc.AddScoped<ICreateSampleData, CreateSampleData>();
        }

        private static IServiceProvider CreateServiceProvider()
        {
            // create service collection
            IServiceCollection isc = new ServiceCollection();
            ConfigureServices(isc);

            // create service provider
            return isc.BuildServiceProvider();
        }
    }
}