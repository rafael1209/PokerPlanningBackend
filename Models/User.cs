using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokerPlanningBackend.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("hashedPassword")]
        public string? HashedPassword { get; set; }

        [BsonElement("avatarUrl")]
        public string? AvatarUrl { get; set; }

        [BsonElement("authToken")]
        public required string AuthToken { get; set; }

        [BsonElement("createdAtUtc")]
        public DateTime CreatedAtUtc { get; set; }
    }
}