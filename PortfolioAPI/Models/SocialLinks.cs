using MongoDB.Bson.Serialization.Attributes;

namespace TeamPortfolio.Models
{
    public class SocialLinks
    {
        [BsonElement("linkedin")]
        public string LinkedIn { get; set; }
        [BsonElement("github")]
        public string GitHub { get; set; }
        [BsonElement("gitlab")]
        public string GitLab { get; set; }
    }
}