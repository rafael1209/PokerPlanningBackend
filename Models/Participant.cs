using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokerPlanningBackend.Models
{
    public class Participant
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("avatarUrl")]
        public string? AvatarUrl { get; set; }

        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }
    }
}
