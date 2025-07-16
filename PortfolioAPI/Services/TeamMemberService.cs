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

        public async Task<TeamMember> GetAsync(string id) =>
            await _teamMembers.Find<TeamMember>(member => member.Id == id).FirstOrDefaultAsync();

        public async Task<TeamMember> CreateAsync(TeamMember teamMember)
        {
            await _teamMembers.InsertOneAsync(teamMember);
            return teamMember;
        }

        public async Task UpdateAsync(string id, TeamMember teamMemberIn) =>
            await _teamMembers.ReplaceOneAsync(member => member.Id == id, teamMemberIn);

        public async Task RemoveAsync(string id) =>
            await _teamMembers.DeleteOneAsync(member => member.Id == id);
    }
}