using Presentation.Context;
using Presentation.Services;

namespace GoldenRaspberryAwards.Api.Extensions;

public static class AppExtensions
{
    public static void ImportMoviesFromCsvToDatabase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var csvImportService = scope.ServiceProvider.GetRequiredService<CsvImportService>();

            dbContext.Database.EnsureCreated();

            if (!dbContext.Movies.Any())
                csvImportService.ImportCsvToDatabase();
        }
    }
}