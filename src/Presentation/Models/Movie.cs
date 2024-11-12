using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Presentation.Models;

public class Movie
{
    [Key]
    [Ignore]
    [JsonIgnore]
    public Guid Id { get; set; }

    [Name("year")]
    public int Year { get; set;}

    [Name("title")]
    public string? Title { get; set; }

    [Name("studios")]
    public string? Studios { get; set; }

    [Name("producers")]
    public string? Producers { get; set; }

    [Name("winner")]
    public string? Winner { get; set; }
}