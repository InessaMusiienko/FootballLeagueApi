using System.ComponentModel.DataAnnotations;
using static FootballLeagueApi.Models.DataValidations;

namespace FootballLeagueApi.Models.ApiModels
{
    public class MatchDTO
    {
        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string HomeTeam { get; set; } = null!;

        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string GuestTeam { get; set; } = null!;

        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string Winner { get; set; } = null!;

    }
}
