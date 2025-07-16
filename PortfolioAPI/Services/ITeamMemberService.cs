using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPortfolio.Models;

namespace TeamPortfolio.Services
{
    public interface ITeamMemberService
    {
        Task<List<TeamMember>> GetAsync();
        Task<TeamMember> GetAsync(string memberId);
        Task<TeamMember> CreateAsync(TeamMember teamMember);
        Task UpdateAsync(string memberId, TeamMember teamMember);
        Task RemoveAsync(string memberId);
    }
}