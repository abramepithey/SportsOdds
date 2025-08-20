using Api.Repositories;
using Core;
using Xunit;
using System.Linq;

namespace Test
{
    public class OddsRepoTests
    {
        [Fact]
        public void GetAll_ReturnsSeededData()
        {
            var repo = new OddsRepository();
            var all = repo.GetAll().ToList();
            Assert.True(all.Count >= 2);
            Assert.Contains(all, o => o.GameId == "GAME1");
            Assert.Contains(all, o => o.GameId == "GAME2");
        }

        [Fact]
        public void GetByGameId_ReturnsCorrectOddsOrNull()
        {
            var repo = new OddsRepository();
            var odds = repo.GetByGameId("GAME1");
            Assert.NotNull(odds);
            Assert.Equal("Lions", odds!.HomeTeam);
            Assert.Null(repo.GetByGameId("NOT_FOUND"));
        }

        [Fact]
        public void Upsert_AddsOrUpdatesOdds_ValidatesInput()
        {
            var repo = new OddsRepository();
            var odds = new GameOdds
            {
                GameId = "GAME3",
                HomeTeam = "Sharks",
                AwayTeam = "Eagles",
                Spread = 1.5m,
                OverUnder = 44.0m,
                LastUpdated = System.DateTimeOffset.UtcNow
            };
            Assert.True(repo.Upsert(odds, out var error));
            var fetched = repo.GetByGameId("GAME3");
            Assert.NotNull(fetched);
            Assert.Equal("Sharks", fetched!.HomeTeam);

            odds.HomeTeam = "";
            Assert.False(repo.Upsert(odds, out error));
            Assert.Equal("HomeTeam is required.", error);
        }
    }
}
