namespace PortfolioAPI.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Link { get; set; }
        public string? GitLink { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<ProjectTech> Techs { get; set; } = new List<ProjectTech>();
    }
}
