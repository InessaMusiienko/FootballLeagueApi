

using System.ComponentModel.DataAnnotations;

namespace FootballLeagueApi.Models.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int TotalPoint { get; set; }

        //public ICollection<Match> HomeMatches { get; set; } = new HashSet<Match>();
        //public ICollection<Match> GuestMatches { get; set; } = new HashSet<Match>();

        public virtual ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public virtual ICollection<Match> GuestMatches { get; set; } = new List<Match>();
    }
}
