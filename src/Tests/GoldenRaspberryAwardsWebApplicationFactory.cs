using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation.Context;

namespace GoldenRaspberryAwards.Api.IntegrationTests;

internal class GoldenRaspberryAwardsWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services => {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            var connString = GetConnectionString();
            services.AddSqlite<AppDbContext>(connString);

            var dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
        });
    }

    private static string? GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.Test.json", false, true)
            .Build();

        return configuration.GetConnectionString("DefaultConnection");
    }

    private static AppDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        return dbContext;
    }
}