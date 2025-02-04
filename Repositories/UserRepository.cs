using System.Security.Cryptography;
using MongoDB.Bson;
using MongoDB.Driver;
using PokerPlanningBackend.Models;
using System.Text;
using PokerPlanningBackend.Database;
using PokerPlanningBackend.Interfaces;

namespace PokerPlanningBackend.Repositories
{
    public class UserRepository(MongoDbContext context) : IUserRepository
    {
        private readonly IMongoCollection<User> _users = context.Users;

        public async Task<List<User>?> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(ObjectId id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await _users.Find(user => user.AuthToken == authToken)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(user => user.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(user => user.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);

            return user;
        }

        public async Task UpdateAsync(ObjectId id, User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == id, user);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var result = await _users.DeleteOneAsync(user => user.Id == id);

            if (result.DeletedCount == 0)
            {
                throw new Exception("User not found.");
            }
        }
    }
}
