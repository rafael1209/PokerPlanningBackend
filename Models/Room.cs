using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PokerPlanningBackend.Models
{
    public class Room
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("roomName")]
        public required string RoomName { get; set; }

        [BsonElement("participants")]
        public List<Participant>? Participants { get; set; }
    }
}
