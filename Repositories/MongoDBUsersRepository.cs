using System;
using System.Collections.Generic;
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

        public void CreateUser(User user)
        {
            userCollection.InsertOne(user);
        }

        public void DeleteUser(Guid id)
        {
            FilterDefinition<User> filter = filterDefinitionBuilder.Eq(existUser => existUser.Id, id);
            userCollection.DeleteOne(filter);
        }

        public User GetUser(Guid id)
        {
            FilterDefinition<User> filter = filterDefinitionBuilder.Eq(existUser => existUser.Id, id);
            return userCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return userCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateUser(User user)
        {
            FilterDefinition<User> filter = filterDefinitionBuilder.Eq(existUser => existUser.Id, user.Id);
            userCollection.ReplaceOne(filter, user);
        }
    }
}