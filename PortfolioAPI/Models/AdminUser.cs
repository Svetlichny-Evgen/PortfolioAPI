using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeamPortfolio.Models
{
    public class AdminUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "Admin";
    }
}