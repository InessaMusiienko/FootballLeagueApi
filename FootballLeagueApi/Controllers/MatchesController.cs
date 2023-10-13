using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballLeagueApi.Data;
using FootballLeagueApi.Models.Entities;

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
            return await _context.Matches.ToListAsync();
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

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch([FromRoute] int id, [FromBody] Match updatedMatch)
        {

            var match = await _context.Matches.FindAsync(id);
            if (match == null || match.IsPlayed == false)
            {
                return NotFound();
            }

            match.Team1 = updatedMatch.Team1;
            match.Team2 = updatedMatch.Team2;
            match.Winner = updatedMatch.Winner;
            match.IsPlayed = updatedMatch.IsPlayed;

            await _context.SaveChangesAsync();
            return Ok(match);
            
        }

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Matches.Add(match);

            if(match.IsPlayed == true)
            {
                var playerFirst = match.Team1; //teamPlovdiv
                var player1 = await _context.Teams.FirstOrDefaultAsync(t => t.Name == playerFirst); //object

                var playerSecond = match.Team2; // teamVarna
                var player2 = await _context.Teams.FirstOrDefaultAsync(t => t.Name == playerSecond); // object

                if(match.Winner == string.Empty)
                {
                    player1.TotalPoint++;
                    player2.TotalPoint++;
                }
                else
                {
                    if (match.Winner == playerFirst)
                    {
                        player1.TotalPoint += 3;
                    }
                    else if (match.Winner == playerSecond)
                    {
                        player2.TotalPoint += 3;
                    }
                }                
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, match);
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
