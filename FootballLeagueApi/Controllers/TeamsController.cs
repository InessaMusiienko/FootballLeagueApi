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
using FootballLeagueApi.Services.ServiceInterfaces;

namespace FootballLeagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly FootballLeagueDbContext _context;
        private ITeamScoreCalculatingService _teamScoreCalculatingService;

        public TeamsController(FootballLeagueDbContext context, ITeamScoreCalculatingService teamScoreCalculatingService)
        {
            _context = context;
            _teamScoreCalculatingService = teamScoreCalculatingService;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            var allTeamsInfo = _context.Teams.Include(x => x.HomeMatches).Include(x => x.GuestMatches).ToList()
                  .Select(x => new TeamDTO { Name = x.Name, TotalPoint = _teamScoreCalculatingService.CalculateScore(x.HomeMatches, x.GuestMatches) })
                  .OrderByDescending(x=>x.TotalPoint).ToList();

            return allTeamsInfo;
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
          if (_context.Teams == null)
          {
              return NotFound();
          }
            var team = await _context.Teams.Include(x => x.HomeMatches)
                .Include(x => x.GuestMatches).FirstOrDefaultAsync(x => x.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            var teamToReturn 
                = new TeamDTO { Name = team.Name, TotalPoint=_teamScoreCalculatingService.CalculateScore(team.HomeMatches,team.GuestMatches) };

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
                Name = team.Name
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
