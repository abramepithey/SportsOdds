using Core;
using System.Collections.Concurrent;

namespace Api.Repositories
{
    public interface IOddsRepository
    {
        IEnumerable<GameOdds> GetAll();
        GameOdds? GetByGameId(string gameId);
        bool Upsert(GameOdds odds, out string? error);
    }

    public class OddsRepository : IOddsRepository
    {
        private readonly ConcurrentDictionary<string, GameOdds> _odds;

        public OddsRepository()
        {
            _odds = new ConcurrentDictionary<string, GameOdds>(
                new[]
                {
                    new KeyValuePair<string, GameOdds>("GAME1", new GameOdds
                    {
                        GameId = "GAME1",
                        HomeTeam = "Lions",
                        AwayTeam = "Tigers",
                        Spread = -3.5m,
                        OverUnder = 48.5m,
                        LastUpdated = DateTimeOffset.UtcNow
                    }),
                    new KeyValuePair<string, GameOdds>("GAME2", new GameOdds
                    {
                        GameId = "GAME2",
                        HomeTeam = "Bears",
                        AwayTeam = "Wolves",
                        Spread = 2.0m,
                        OverUnder = 52.0m,
                        LastUpdated = DateTimeOffset.UtcNow
                    })
                }
            );
        }

        public IEnumerable<GameOdds> GetAll() => _odds.Values;

        public GameOdds? GetByGameId(string gameId)
        {
            _odds.TryGetValue(gameId, out var odds);
            return odds;
        }

        public bool Upsert(GameOdds odds, out string? error)
        {
            if (!odds.IsValid(out error)) return false;
            odds.LastUpdated = DateTimeOffset.UtcNow;
            _odds.AddOrUpdate(odds.GameId, odds, (k, v) => odds);
            return true;
        }
    }
}
