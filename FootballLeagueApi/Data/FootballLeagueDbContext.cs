using FootballLeagueApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueApi.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;

       
    }
}
