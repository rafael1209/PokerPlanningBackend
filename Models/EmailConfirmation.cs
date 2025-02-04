using MongoDB.Bson.Serialization.Attributes;

namespace PokerPlanningBackend.Models;

public class EmailConfirmation
{
    [BsonElement("token")]
    public required string Token { get; set; }

    [BsonElement("expiresAtUtc")]
    public required DateTime ExpiresAtUtc { get; set; }
}