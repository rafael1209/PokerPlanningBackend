using MongoDB.Driver;
using PokerPlanningBackend.Interfaces;
using PokerPlanningBackend.Models;

namespace PokerPlanningBackend.Database
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        private const string ConstUsersCollection = "users";
        private const string ConstTempUsersCollection = "temp-users";

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDb:Name"));
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(ConstUsersCollection);
        public IMongoCollection<TempUser> TempUsers => _database.GetCollection<TempUser>(ConstTempUsersCollection);

    }
}