using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPortfolio.Models;

namespace TeamPortfolio.Services
{
    public interface ITeamMemberService
    {
        Task<List<TeamMember>> GetAsync();
        Task<TeamMember> GetAsync(int id);
        Task<TeamMember> CreateAsync(TeamMember teamMember);
        Task UpdateAsync(int id, TeamMember teamMember);
        Task RemoveAsync(int id);
    }
}