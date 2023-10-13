using FootballLeagueApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace FootballLeagueApi.Data
{
    public class FootballLeagueDbContext : DbContext
    {
        public FootballLeagueDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Match>()
            //    .HasOne(x=>x.HomeTeam).WithMany(x=>x.HomeMatches).HasForeignKey(x=>x.HomeTeamId)/*.OnDelete(DeleteBehavior.NoAction)*/;
            //modelBuilder.Entity<Match>()
            //    .HasOne(x => x.GuestTeam).WithMany(x => x.GuestMatches).HasForeignKey(x => x.GuestTeamId)/*.OnDelete(DeleteBehavior.NoAction)*/;

            modelBuilder.Entity<Match>()
                    .HasOne(m => m.HomeTeam)
                    .WithMany(t => t.HomeMatches)
                    .HasForeignKey(m => m.HomeTeamId)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                        .HasOne(m => m.GuestTeam)
                        .WithMany(t => t.GuestMatches)
                        .HasForeignKey(m => m.GuestTeamId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>().Property(x => x.Winner).HasConversion<string>();
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }

       
    }
}
