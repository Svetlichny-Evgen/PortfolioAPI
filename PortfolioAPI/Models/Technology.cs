namespace PortfolioAPI.Models
{
    public class Technology
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<ProjectTech> Projects { get; set; } = new List<ProjectTech>();
    }
}
