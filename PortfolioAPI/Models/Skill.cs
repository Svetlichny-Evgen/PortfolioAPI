namespace PortfolioAPI.Models
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public SkillsCategory Category { get; set; } = null!;

    }
}
