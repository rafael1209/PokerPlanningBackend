using MongoDB.Bson;
using MongoDB.Driver;
using PokerPlanningBackend.Database;
using PokerPlanningBackend.Interfaces;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Repositories
{
    public class RoomRepository(MongoDbContext context) : IRoomRepository
    {
        private readonly IMongoCollection<Room> _rooms = context.Room;

        public async Task<List<Room>?> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public Task<Room?> GetByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Room room)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ObjectId id, Room room)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
