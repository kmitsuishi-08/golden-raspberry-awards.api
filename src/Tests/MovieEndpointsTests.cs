using FluentAssertions;
using GoldenRaspberryAwards.Api.Models;
using GoldenRaspberryAwards.Api.Responses;
using System.Net.Http.Json;

namespace GoldenRaspberryAwards.Api.IntegrationTests;

public class MovieEndpointsTests
{
    private readonly MovieProducerResponse _expectedResponse;

    public MovieEndpointsTests()
    {
        var min = new List<MovieProducer> { new(producer: "Joel Silver", interval: 1, previousWin: 1990, followingWin: 1991) };
        var max = new List<MovieProducer> { new(producer: "Matthew Vaughn", interval: 13, previousWin: 2002, followingWin: 2015) };
        _expectedResponse = new MovieProducerResponse(min: min, max: max);
    }

    [Fact]
    public async Task GetProducersMovies_Returns_MinAndMaxIntervalsBetweenAwards()
    {
        var application = new GoldenRaspberryAwardsWebApplicationFactory();
        var client = application.CreateClient();
        var response = await client.GetAsync("api/movies/producers");

        response.EnsureSuccessStatusCode();

        var matchResponse = await response.Content.ReadFromJsonAsync<MovieProducerResponse>();
        matchResponse.Should().BeEquivalentTo(_expectedResponse);
    }
}