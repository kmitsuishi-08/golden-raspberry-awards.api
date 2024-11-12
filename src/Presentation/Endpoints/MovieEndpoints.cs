using GoldenRaspberryAwards.Api.Models;
using GoldenRaspberryAwards.Api.Responses;
using Microsoft.EntityFrameworkCore;
using Presentation.Context;

namespace GoldenRaspberryAwards.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieEndpoints(this WebApplication app)
        {
            app.MapGet("api/movies/producers", async (AppDbContext db) =>
            {
                var winningAwards = await db.Movies.Where(m => !string.IsNullOrEmpty(m.Winner)).ToListAsync();

                if (winningAwards == null || !winningAwards.Any())
                    return Results.NotFound();

                var producerAwards = winningAwards
                    .SelectMany(w => w.Producers.Split(new[] { ",", "and" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => new { Producer = p.Trim(), w.Year }))
                    .GroupBy(pa => pa.Producer)
                    .Where(g => g.Count() > 1)
                    .Select(g =>
                        g.OrderBy(p => p.Year)
                        .Zip(g.OrderBy(p => p.Year).Skip(1), (first, second) => new MovieProducer(producer: g.Key, interval: second.Year - first.Year, previousWin: first.Year, followingWin: second.Year))
                    );

                if (producerAwards == null || !producerAwards.Any())
                    return Results.NotFound();

                var minInterval = producerAwards.SelectMany(p => p).Min(p => p.Interval);
                var maxInterval = producerAwards.SelectMany(p => p).Max(p => p.Interval);

                var minIntervalProducers = producerAwards.SelectMany(p => p).Where(p => p.Interval == minInterval);
                var maxIntervalProducers = producerAwards.SelectMany(p => p).Where(p => p.Interval == maxInterval);

                return Results.Ok(new MovieProducerResponse(min: minIntervalProducers, max: maxIntervalProducers));
            })
            .WithName("GetProducersMovies")
            .WithDescription("Get producers with min and max intervals between awards")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}