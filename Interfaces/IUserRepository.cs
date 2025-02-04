using MongoDB.Bson;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Interfaces;

public interface IUserRepository
{
    Task<List<User>?> GetAllAsync();
    Task<User?> GetByIdAsync(ObjectId id);
    Task<User?> GetByAuthTokenAsync(string authToken);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string email);
    Task<User?> CreateAsync(User user);
    Task UpdateAsync(ObjectId id, User user);
    Task DeleteAsync(ObjectId id);
}