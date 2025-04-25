namespace PortfolioAPI.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Qualification { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Linkedin { get; set; }
        public string? Github { get; set; }
        public string? Photo { get; set; }

        public List<SkillDto> Skills { get; set; } = new();
        public List<ProjectDto> Projects { get; set; } = new();
    }
}
