using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokerPlanningBackend.Models
{
    public class TempUser
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("hashedPassword")]
        public required string HashedPassword { get; set; }

        [BsonElement("emailConfirmation")]
        public required EmailConfirmation EmailConfirmation { get; set; }

        [BsonElement("createdAtUtc")]
        public DateTime CreatedAtUtc { get; set; }
    }
}
