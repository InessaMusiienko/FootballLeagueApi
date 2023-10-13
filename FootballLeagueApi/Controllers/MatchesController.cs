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
            if (!_context.Matches.Any(m => m.Id == id))
            {
                return BadRequest();
            }

            var match = await _context.Matches.FindAsync(id);

            var guestTeam = await _context.Teams
                .FirstOrDefaultAsync(t => t.Name.ToLower() == updatedMatch.GuestTeam.ToLower());
            var homeTeam = await _context.Teams
                .FirstOrDefaultAsync(t => t.Name.ToLower() == updatedMatch.HomeTeam.ToLower());

            guestTeam.Name = updatedMatch.GuestTeam;
            homeTeam.Name = updatedMatch.GuestTeam;
            
            //points?

           
            await _context.SaveChangesAsync();
            return Ok(match);
            
        }

        // POST: api/Matches
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch (MatchDTO match)
        {
            if (match.IsPlayed == true)
            {
                var homeTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Name == match.HomeTeam);
                var guestTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Name == match.GuestTeam);

                if (match.Winner == string.Empty || match.Winner.ToLower() == "draw")
                {
                    homeTeam.TotalPoint++;                    
                    guestTeam.TotalPoint++;
                }

                else if(match.Winner.ToLower() == homeTeam.Name.ToLower())
                {
                    homeTeam.TotalPoint += 3;
                }

                else
                {
                    guestTeam.TotalPoint += 3;
                }
            }
            else
            {
                return BadRequest();
            }
            await _context.SaveChangesAsync();

            return Ok();
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
