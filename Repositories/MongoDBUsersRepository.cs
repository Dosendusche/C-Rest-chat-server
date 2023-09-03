using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C_Rest_chat_server.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace C_Rest_chat_server.Repositories
{
    public class MongoDBUsersRepository : IUsersRepository
    {
        private const string databaseName = "c_rest_chat";
        private const string collectionName = "users";
        private readonly IMongoCollection<User> userCollection;
        private readonly FilterDefinitionBuilder<User> filterDefinitionBuilder = Builders<User>.Filter;

        public MongoDBUsersRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            userCollection = database.GetCollection<User>(collectionName);
        }

        public async Task CreateUserAsync(User user)
        {
            await userCollection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            FilterDefinition<User> filter = filterDefinitionBuilder.Eq(existUser => existUser.Id, id);
            await userCollection.DeleteOneAsync(filter);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            FilterDefinition<User> filter = filterDefinitionBuilder.Eq(existUser => existUser.Id, id);
            return await userCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            FilterDefinition<User> filter = filterDefinitionBuilder.Eq(existUser => existUser.Id, user.Id);
            await userCollection.ReplaceOneAsync(filter, user);
        }
    }
}