using FootballLeagueApi.Models.Entities;

namespace FootballLeagueApi.Services.ServiceInterfaces
{
    public interface ITeamScoreCalculatingService
    {
        public int CalculateScore(ICollection<Match> homeMatches, ICollection<Match> guestMatches);
    }
}
