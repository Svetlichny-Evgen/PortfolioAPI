using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Data;
using PortfolioAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SkillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/skill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return await _context.Skills.Include(s => s.Category).ToListAsync();
        }

        // GET: api/skill/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(Guid id)
        {
            var skill = await _context.Skills.Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // POST: api/skill
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        // PUT: api/skill/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(Guid id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Skills.Any(e => e.Id == id))
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

        // DELETE: api/skill/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(Guid id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
