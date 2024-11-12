using Presentation.Context;
using Microsoft.EntityFrameworkCore;
using Presentation.Services;
using GoldenRaspberryAwards.Api.Extensions;
using GoldenRaspberryAwards.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<CsvImportService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.ImportMoviesFromCsvToDatabase();
app.MapMovieEndpoints();

app.Run();