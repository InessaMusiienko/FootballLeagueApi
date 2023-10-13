using System.ComponentModel.DataAnnotations;
using static FootballLeagueApi.Models.DataValidations;

namespace FootballLeagueApi.Models.ApiModels
{
    public class TeamDTO
    {
        [Required(ErrorMessage = "Team name is required.")]
        [MaxLength(TeamMaxLength, ErrorMessage = "Team name cannot be longer than 15 characters.")]
        public string Name { get; set; } = null!;

        public int TotalPoint { get; set; }
    }
}
