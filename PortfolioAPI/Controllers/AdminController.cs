using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeamPortfolio.DTOs;
using TeamPortfolio.Models;
using TeamPortfolio.Services;

namespace TeamPortfolio.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;
        private readonly IAdminService _adminService;

        public AdminController(ITeamMemberService teamMemberService, IAdminService adminService)
        {
            _teamMemberService = teamMemberService;
            _adminService = adminService;
        }

        [HttpPost("team-member/{id}/projects")]
        public async Task<IActionResult> AddProject(int id, [FromBody] Project project)
        {
            var teamMember = await _teamMemberService.GetAsync(id);
            if (teamMember == null)
                return NotFound();

            teamMember.Projects.Add(project);
            await _teamMemberService.UpdateAsync(id, teamMember);

            return Ok(teamMember);
        }

        [HttpPut("team-member/{memberId}/projects/{projectId}")]
        public async Task<IActionResult> UpdateProject(int memberId, string projectId, [FromBody] Project project)
        {
            var teamMember = await _teamMemberService.GetAsync(memberId);
            if (teamMember == null)
                return NotFound();

            var existingProject = teamMember.Projects.Find(p => p.Name == projectId);
            if (existingProject == null)
                return NotFound();

            // Обновление проекта
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.Link = project.Link;
            existingProject.Technologies = project.Technologies;

            await _teamMemberService.UpdateAsync(memberId, teamMember);

            return Ok(teamMember);
        }

        [HttpPost("team-member/{id}/skills")]
        public async Task<IActionResult> AddSkill(int id, [FromBody] SkillUpdateDTO skillUpdate)
        {
            var teamMember = await _teamMemberService.GetAsync(id);
            if (teamMember == null)
                return NotFound();

            // Логика добавления навыка в соответствующую категорию
            switch (skillUpdate.Category.ToLower())
            {
                case "programming_languages":
                    teamMember.Skills.ProgrammingLanguages.Add(skillUpdate.Skill);
                    break;
                case "technologies":
                    teamMember.Skills.Technologies.Add(skillUpdate.Skill);
                    break;
                // Добавьте другие категории по аналогии
                default:
                    return BadRequest("Invalid skill category");
            }

            await _teamMemberService.UpdateAsync(id, teamMember);

            return Ok(teamMember);
        }
    }
}