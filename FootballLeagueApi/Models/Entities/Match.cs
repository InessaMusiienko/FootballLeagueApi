using FootballLeagueApi.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballLeagueApi.Models.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey(nameof(HomeTeamId))]
        //public int HomeTeamId { get; set; }
        //public Team HomeTeam { get; set; } = null!;

        //[ForeignKey(nameof(GuestTeamId))]
        //public int GuestTeamId { get; set; }
        //public Team GuestTeam { get; set; } = null!;

        [ForeignKey(nameof(HomeTeamId))]
        public int HomeTeamId { get; set;}

        [ForeignKey(nameof(GuestTeamId))]
        public int GuestTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team GuestTeam { get; set; }

        public Winner Winner { get; set; }
        
        public bool IsPlayed { get; set; }

    }
}
