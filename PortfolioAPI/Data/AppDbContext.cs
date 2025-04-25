using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillsCategory> SkillsCategories { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTech> ProjectTechs { get; set; }
    }
}
