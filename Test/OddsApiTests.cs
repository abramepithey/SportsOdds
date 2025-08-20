using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Api.DTOs;

namespace Test
{
    public class OddsApiTests : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        private readonly WebApplicationFactory<Api.Program> _factory;
        public OddsApiTests(WebApplicationFactory<Api.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllOdds_ReturnsSeededOdds()
        {
            var client = _factory.CreateClient();
            var odds = await client.GetFromJsonAsync<GameOddsDto[]>("/api/odds");
            Assert.NotNull(odds);
            Assert.Contains(odds!, o => o.GameId == "GAME1");
            Assert.Contains(odds!, o => o.GameId == "GAME2");
        }

        [Fact]
        public async Task GetOddsByGameId_ReturnsCorrectOr404()
        {
            var client = _factory.CreateClient();
            var odds = await client.GetFromJsonAsync<GameOddsDto>("/api/odds/GAME1");
            Assert.NotNull(odds);
            Assert.Equal("Lions", odds!.HomeTeam);

            var response = await client.GetAsync("/api/odds/NOT_FOUND");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostOdds_UpsertsOrValidates()
        {
            var client = _factory.CreateClient();
            var upsert = new UpsertGameOddsDto
            {
                GameId = "GAME4",
                HomeTeam = "Panthers",
                AwayTeam = "Falcons",
                Spread = 2.5m,
                OverUnder = 50.0m
            };
            var post = await client.PostAsJsonAsync("/api/odds", upsert);
            post.EnsureSuccessStatusCode();
            var odds = await post.Content.ReadFromJsonAsync<GameOddsDto>();
            Assert.NotNull(odds);
            Assert.Equal("Panthers", odds!.HomeTeam);

            upsert.HomeTeam = "";
            var badPost = await client.PostAsJsonAsync("/api/odds", upsert);
            Assert.Equal(HttpStatusCode.BadRequest, badPost.StatusCode);
            var errorObj = await badPost.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            Assert.Equal("HomeTeam is required.", errorObj!["error"]);
        }
    }
}
