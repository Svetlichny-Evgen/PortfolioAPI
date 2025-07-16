using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamPortfolio.Models
{
    public class Project
    {
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("links")]
        public string[] Links { get; set; }
    }
}