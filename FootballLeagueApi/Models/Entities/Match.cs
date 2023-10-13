namespace FootballLeagueApi.Models.Entities
{
    public class Match
    {
        public int Id { get; set; }

        public string Team1 { get; set; } = null!;

        public string Team2 { get; set; } = null!;

        public string Winner { get; set; }
        public bool IsPlayed { get; set; }

    }
}
