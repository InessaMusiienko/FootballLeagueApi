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

namespace FootballLeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly FootballLeagueDbContext _context;

        public TeamsController(FootballLeagueDbContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            return await _context.Teams.OrderByDescending(t=>t.TotalPoint).ThenByDescending(t=>t.Name).ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, TeamDTO updatedTeam)
        {
            if(!_context.Teams.Any(t=>t.Id == id))
            {
                return BadRequest();
            }

            var team = await _context.Teams.FirstOrDefaultAsync(t=>t.Id == id);

            team.Name = updatedTeam.Name;
            team.TotalPoint = updatedTeam.TotalPoint;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teams
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(TeamDTO team)
        {
            Team newTeam = new Team()
            {
                Name = team.Name,
                TotalPoint = team.TotalPoint
            };

            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();

            return Ok(newTeam);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (_context.Teams == null)
            {
                return NotFound();
            }
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TeamExists(int id)
        {
            return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
