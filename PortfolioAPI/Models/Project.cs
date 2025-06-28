using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamPortfolio.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public List<string> Technologies { get; set; }
    }
}