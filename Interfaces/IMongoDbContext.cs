using MongoDB.Driver;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Interfaces;

public interface IMongoDbContext
{
    IMongoCollection<User> Users { get; }
}