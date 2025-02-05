using MongoDB.Bson;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>?> GetAllAsync();
        Task<Room?> GetByIdAsync(ObjectId id);
        Task CreateAsync(Room room);
        Task UpdateAsync(ObjectId id, Room room);
        Task DeleteAsync(ObjectId id);
    }
}
