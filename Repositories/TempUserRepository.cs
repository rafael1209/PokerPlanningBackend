using MongoDB.Bson;
using MongoDB.Driver;
using PokerPlanningBackend.Database;
using PokerPlanningBackend.Interfaces;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Repositories
{
    public class TempUserRepository(MongoDbContext context) : ITempUserRepository
    {
        private readonly IMongoCollection<TempUser> _tempUsers = context.TempUsers;

        public async Task<TempUser?> GetByIdAsync(ObjectId id)
        {
            return await _tempUsers.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TempUser?> GetByEmailAsync(string email)
        {
            return await _tempUsers.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<TempUser?> GetByUsernameAsync(string username)
        {
            return await _tempUsers.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TempUser user)
        {
            await _tempUsers.InsertOneAsync(user);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _tempUsers.DeleteOneAsync(user => user.Id == id);
        }
    }
}
