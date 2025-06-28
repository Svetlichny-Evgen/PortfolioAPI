using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamPortfolio.Models;

namespace TeamPortfolio.Services
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly IMongoCollection<TeamMember> _teamMembers;

        public TeamMemberService(
            ITeamPortfolioDatabaseSettings settings,
            IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _teamMembers = database.GetCollection<TeamMember>(settings.TeamMembersCollectionName);
        }

        public async Task<List<TeamMember>> GetAsync() =>
            await _teamMembers.Find(member => true).ToListAsync();

        public async Task<TeamMember> GetAsync(int id) =>
            await _teamMembers.Find<TeamMember>(member => member.MemberId == id).FirstOrDefaultAsync();

        public async Task<TeamMember> CreateAsync(TeamMember teamMember)
        {
            await _teamMembers.InsertOneAsync(teamMember);
            return teamMember;
        }

        public async Task UpdateAsync(int id, TeamMember teamMemberIn) =>
            await _teamMembers.ReplaceOneAsync(member => member.MemberId == id, teamMemberIn);

        public async Task RemoveAsync(int id) =>
            await _teamMembers.DeleteOneAsync(member => member.MemberId == id);
    }
}