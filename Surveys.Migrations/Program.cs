using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Surveys.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = CreateService();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateService()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("configs/connectionStrings_dev.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("Survey")
                ?? "Host=db;Port=5432;Database=surveys_dev;Username=postgres;Password=postgres";

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres15_0()
                    .WithGlobalConnectionString(connectionString)
                    .WithGlobalCommandTimeout(TimeSpan.FromMinutes(5))
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
