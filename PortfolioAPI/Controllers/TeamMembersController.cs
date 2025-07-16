using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamPortfolio.DTOs;
using TeamPortfolio.Models;
using TeamPortfolio.Services;

namespace TeamPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMembersController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamMember>>> Get()
        {
            var teamMembers = await _teamMemberService.GetAsync();
            return Ok(teamMembers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMember>> Get(string id)
        {
            var teamMember = await _teamMemberService.GetAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return Ok(teamMember);
        }

        [HttpPost]
        public async Task<ActionResult<TeamMember>> Create(TeamMemberCreateDTO teamMemberCreateDTO)
        {
            var teamMember = new TeamMember
            {
                FullName = teamMemberCreateDTO.FullName,
                BirthDate = teamMemberCreateDTO.BirthDate,
                Position = teamMemberCreateDTO.Position,
                Email = teamMemberCreateDTO.Email,
                SocialLinks = teamMemberCreateDTO.SocialLinks,
                PhotoPath = teamMemberCreateDTO.PhotoPath,
                Skills = teamMemberCreateDTO.Skills,
                Projects = teamMemberCreateDTO.Projects
            };

            await _teamMemberService.CreateAsync(teamMember);

            return CreatedAtAction(nameof(Get), new { id = teamMember.Id }, teamMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, TeamMemberUpdateDTO teamMemberUpdateDTO)
        {
            var existingTeamMember = await _teamMemberService.GetAsync(id);

            if (existingTeamMember == null)
            {
                return NotFound();
            }

            existingTeamMember.FullName = teamMemberUpdateDTO.FullName;
            existingTeamMember.BirthDate = teamMemberUpdateDTO.BirthDate;
            existingTeamMember.Position = teamMemberUpdateDTO.Position;
            existingTeamMember.Email = teamMemberUpdateDTO.Email;
            existingTeamMember.SocialLinks = teamMemberUpdateDTO.SocialLinks;
            existingTeamMember.PhotoPath = teamMemberUpdateDTO.PhotoPath;
            existingTeamMember.Skills = teamMemberUpdateDTO.Skills;
            existingTeamMember.Projects = teamMemberUpdateDTO.Projects;

            await _teamMemberService.UpdateAsync(id, existingTeamMember);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var teamMember = await _teamMemberService.GetAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            await _teamMemberService.RemoveAsync(id);

            return NoContent();
        }
    }
}