using Microsoft.EntityFrameworkCore;
using Surveys.DataAccess;
using Surveys.DataAccess.DbContexts;
using Surveys.Logic;
using System.Reflection;

namespace Surveys.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = new ConfigurationBuilder()
                .AddJsonFile("configs/connectionStrings_dev.json")
                .Build();

            builder.Services
                .AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(config.GetConnectionString("Survey"))
                    .UseSnakeCaseNamingConvention())
                .AddScoped<IDbInitializer, DbInitializer>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var queryTypes = typeof(IQuery<,>).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQuery<,>)));

            foreach (var type in queryTypes)
            {
                builder.Services.AddScoped(type);
            }

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await initializer.InitializeAsync();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            app.Run();
        }
    }
}
