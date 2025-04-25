using System.Collections.Generic;

namespace PortfolioAPI.DTOs
{
    public class ProjectDto
    {
        public string Type { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Link { get; set; }
        public string? GitLink { get; set; }

        public List<string> TechTitles { get; set; } = new();
    }
}
