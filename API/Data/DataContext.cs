using MongoDB.Driver;
using API.Entities;

namespace API.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database;
        public DataContext(IMongoClient client)
        {
            _database = client.GetDatabase("Database");
        }
        public IMongoCollection<Todo> Todos => _database.GetCollection<Todo>("Todos");
    }
} 