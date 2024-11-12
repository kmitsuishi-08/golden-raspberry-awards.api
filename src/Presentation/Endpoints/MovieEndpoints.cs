using Microsoft.EntityFrameworkCore;
using Presentation.Context;

namespace GoldenRaspberryAwards.Api.Endpoints
{
    public static class MovieEndpoints
    {
        public static void MapMovieEndpoints(this WebApplication app) 
        {
            app.MapGet("api/movies/producers", async (AppDbContext db) => {
                //Get only producers who won
                var winningAwards = await db.Movies.Where(m => !string.IsNullOrEmpty(m.Winner)).ToListAsync();

                if (winningAwards == null || !winningAwards.Any())
                    return Results.NotFound();

                //Split producers name 
                //Separate the producers and the respective years in which they won and group them together
                //Only take those who have won more than once
                //Sort the years in ascending order
                //Calculate the interval between years
                var producerAwards = winningAwards
                    .SelectMany(w => w.Producers.Split(new[] { ",", "and" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => new { Producer = p.Trim(), w.Year }))
                    .GroupBy(pa => pa.Producer)
                    .Where(g => g.Count() > 1)
                    .Select(g =>
                        g.OrderBy(p => p.Year)
                        .Zip(g.OrderBy(p => p.Year).Skip(1), (first, second) => new { Producer = g.Key, Interval = second.Year - first.Year, PreviousWin = first.Year, FollowingWin = second.Year })
                    );

                if (producerAwards == null || !producerAwards.Any())
                    return Results.NotFound();

                //Get a minInterval
                var minInterval = producerAwards.SelectMany(x => x).Min(x => x.Interval);
                //Get a maxInterval
                var maxInterval = producerAwards.SelectMany(x => x).Max(x => x.Interval);

                //Get a minIntervalProducers
                var minIntervalProducers = producerAwards.SelectMany(x => x)
                    .Where(x => x.Interval == minInterval)
                    .Select(x => new
                    {
                        producer = x.Producer,
                        interval = x.Interval,
                        previousWin = x.PreviousWin,
                        followingWin = x.FollowingWin
                    });

                //Get a maxIntervalProducers
                var maxIntervalProducers = producerAwards.SelectMany(x => x)
                    .Where(x => x.Interval == maxInterval)
                    .Select(x => new
                    {
                        producer = x.Producer,
                        interval = x.Interval,
                        previousWin = x.PreviousWin,
                        followingWin = x.FollowingWin
                    });

                return Results.Ok(new
                {
                    min = minIntervalProducers,
                    max = maxIntervalProducers
                });
            })
            .WithName("GetProducersMovies")
            .WithDescription("Get producers with min and max intervals between awards")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();
        }
    }
}
