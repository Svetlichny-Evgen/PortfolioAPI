namespace PortfolioAPI.Models
{
    public class ProjectTech
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public Guid TechId { get; set; }
        public Technology Tech { get; set; } = null!;
    }
}
