namespace PortfolioAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? BirthDate { get; set; }
        public string? Qualification { get; set; }
        public string Email { get; set; }
        public string? LinkedIn { get; set; }
        public string? Github { get; set; }
        public string? Gitlab { get; set; }
        public string? Photo { get; set; }
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();

    }
}
