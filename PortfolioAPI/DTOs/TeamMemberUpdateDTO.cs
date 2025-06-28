using TeamPortfolio.Models;

namespace TeamPortfolio.DTOs
{
    public class TeamMemberUpdateDTO
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public SocialLinks SocialLinks { get; set; }
        public string PhotoPath { get; set; }
        public Skills Skills { get; set; }
        public List<Project> Projects { get; set; }
    }
}