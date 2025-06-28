namespace TeamPortfolio.Models
{
    public interface ITeamPortfolioDatabaseSettings
    {
        string TeamMembersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

    public class TeamPortfolioDatabaseSettings : ITeamPortfolioDatabaseSettings
    {
        public string TeamMembersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}