using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballLeagueApi.Data;
using FootballLeagueApi.Models.Entities;
using FootballLeagueApi.Models.ApiModels;
using FootballLeagueApi.Models;

namespace FootballLeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly FootballLeagueDbContext _context;

        public MatchesController(FootballLeagueDbContext context) // constructor injection (Design pattern)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
          if (_context.Matches == null)
          {
              return NotFound();
          }
            return await _context.Matches.Where(m=>m.IsPlayed == true).ToListAsync();
            //use dto?
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
          if (_context.Matches == null)
          {
              return NotFound();
          }
            var match = await _context.Matches.FindAsync(id);

            if (match == null || match.IsPlayed == false)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Matches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, MatchDTO updatedMatch)
        {
            var match = await _context.Matches.Include(x=>x.HomeTeam).Include(x=>x.GuestTeam).FirstOrDefaultAsync(x=>x.Id == id);

            if(match == null)
            {
                return BadRequest();
            }

            Team updateHomeTeam = null;
            Team updateGuestTeam = null;

            if (!string.IsNullOrEmpty(updatedMatch.HomeTeam))
            {
                var homeTeamExist = await _context.Teams.FirstOrDefaultAsync(x=>x.Name.ToLower() == updatedMatch.HomeTeam.ToLower());
                
                if(homeTeamExist == null)
                {
                    return BadRequest();
                }
                updateHomeTeam = homeTeamExist;
            }

            if (!string.IsNullOrEmpty(updatedMatch.GuestTeam))
            {
                
                var guestTeamExist = await _context.Teams.FirstOrDefaultAsync(x => x.Name.ToLower() == updatedMatch.GuestTeam.ToLower());
                
                if (guestTeamExist == null)
                {
                    return BadRequest();
                }
                updateGuestTeam = guestTeamExist;
            }


            if (string.IsNullOrEmpty(updatedMatch.Winner))
            {
                return BadRequest();
            }

            match.HomeTeam = updateHomeTeam ?? match.HomeTeam;
            match.GuestTeam = updateGuestTeam ?? match.GuestTeam;
            try
            {
                match.Winner = Enum.Parse<Winner>(updatedMatch.Winner.ToLower());
            }
            catch (Exception)
            {
                match.Winner = match.Winner;
            }
           
            await _context.SaveChangesAsync();
            return Ok();            
        }

        // POST: api/Matches
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch (MatchDTO match)
        {

            if (new string[] {match.Winner,match.HomeTeam,match.GuestTeam}.Any(x=>string.IsNullOrEmpty(x)))
            {
                return BadRequest();
            }
            var homeTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Name.ToLower() == match.HomeTeam.ToLower());
            var guestTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Name.ToLower() == match.GuestTeam.ToLower());
            if (homeTeam==null || guestTeam==null)
            {
                return BadRequest();
            }
            var inputWinner = match.Winner.ToLower();
            if(inputWinner!=homeTeam.Name.ToLower() &&
                 inputWinner != guestTeam.Name.ToLower())              
            {
                if (inputWinner != "draw")
                {
                    return BadRequest();
                }
            }

            var dbMatch = new Match { IsPlayed = true, HomeTeam = homeTeam, GuestTeam = guestTeam, Winner = Enum.Parse<Winner>(inputWinner) };
            _context.Matches.Add(dbMatch);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool MatchExists(int id)
        {
            return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
