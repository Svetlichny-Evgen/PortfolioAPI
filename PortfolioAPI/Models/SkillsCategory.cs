namespace PortfolioAPI.Models
{
    public class SkillsCategory
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    }
}
