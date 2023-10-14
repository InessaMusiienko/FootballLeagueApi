using FootballLeagueApi.Models;
using FootballLeagueApi.Models.Entities;
using FootballLeagueApi.Services.ServiceInterfaces;

namespace FootballLeagueApi.Services
{
    public class TeamScoreCalculatingService : ITeamScoreCalculatingService
    {
        public int CalculateScore(ICollection<Match> homeMatches, ICollection<Match> guestMatches)
        {
            int totalScore = 0;
            homeMatches.Where(x => x.Winner == Winner.hometeam || x.Winner == Winner.draw).ToList().ForEach(x =>
            {
                if (x.Winner == Winner.hometeam) totalScore += 3;
                else totalScore += 1;
            });
            guestMatches.Where(x => x.Winner == Winner.guestteam || x.Winner == Winner.draw).ToList().ForEach(x =>
            {
                if (x.Winner == Winner.guestteam) totalScore += 3;
                else totalScore += 1;
            });

            return totalScore;
        }
    }
}
