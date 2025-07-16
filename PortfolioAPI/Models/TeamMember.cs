using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamPortfolio.Models
{
    public class TeamMember
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("full_name")]
        public string FullName { get; set; }

        [BsonElement("birth_date")]
        public string BirthDate { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("social_links")]
        public SocialLinks SocialLinks { get; set; }

        [BsonElement("photo_path")]
        public string PhotoPath { get; set; }

        [BsonElement("skills")]
        public Skills Skills { get; set; }

        [BsonElement("projects")]
        public List<Project> Projects { get; set; }
    }
}