using System.ComponentModel.DataAnnotations;
using static FootballLeagueApi.Models.DataValidations;

namespace FootballLeagueApi.Models.ApiModels
{
    public class MatchDTO
    {
        [Required(ErrorMessage = "Team name is required.")]
        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string HomeTeam { get; set; } = null!;

        [Required(ErrorMessage = "Team name is required.")]
        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string GuestTeam { get; set; } = null!;

        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string Winner { get; set; } = null!;

        public bool IsPlayed { get; set; }
    }
}
