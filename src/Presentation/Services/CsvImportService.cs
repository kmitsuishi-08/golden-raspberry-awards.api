using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Presentation.Context;
using Presentation.Models;

namespace Presentation.Services;
public class CsvImportService(AppDbContext dbContext, IConfiguration config)
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IConfiguration _config = config;

    public void ImportCsvToDatabase()
    {
        var filePath = _config.GetValue<string>("FilePath");
        var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName;
        var csvPath = Path.Combine(solutionDirectory, filePath);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ";"
        };

        using (var reader = new StreamReader(csvPath))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<Movie>().ToList();
            foreach (var record in records)
            {
                record.Id = Guid.NewGuid();
            }
            _dbContext.Movies.AddRange(records);
            _dbContext.SaveChanges();
        }
    }
}