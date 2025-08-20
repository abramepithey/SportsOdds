using System;

namespace Core
{
    public class GameOdds
    {
        public string GameId { get; set; } = string.Empty;
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public decimal Spread { get; set; }
        public decimal OverUnder { get; set; }
        public DateTimeOffset LastUpdated { get; set; }

        public bool IsValid(out string? error)
        {
            if (string.IsNullOrWhiteSpace(GameId)) { error = "GameId is required."; return false; }
            if (string.IsNullOrWhiteSpace(HomeTeam)) { error = "HomeTeam is required."; return false; }
            if (string.IsNullOrWhiteSpace(AwayTeam)) { error = "AwayTeam is required."; return false; }
            if (HomeTeam == AwayTeam) { error = "HomeTeam and AwayTeam must be different."; return false; }
            if (Spread < -100 || Spread > 100) { error = "Spread out of range."; return false; }
            if (OverUnder < 0 || OverUnder > 200) { error = "OverUnder out of range."; return false; }
            error = null;
            return true;
        }
    }
}
