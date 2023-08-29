using System;
using System.Collections.Generic;
using System.Linq;
using C_Rest_chat_server.Entities;

namespace C_Rest_chat_server.Repositories
{
    public class InMemUsersRepository : IUsersRepository
    {
        private readonly List<User> users = new()
        {
            new User { Id = Guid.NewGuid(), Name = "user1", Email = "test1@mail.com", Password = "ThisWillLaterBeEncryptedPasswords", CreatedDate = DateTimeOffset.UtcNow },
            new User { Id = Guid.NewGuid(), Name = "user2", Email = "test2@mail.com", Password = "ThisWillLaterBeEncryptedPasswords", CreatedDate = DateTimeOffset.UtcNow },
            new User { Id = Guid.NewGuid(), Name = "user3", Email = "test3@mail.com", Password = "ThisWillLaterBeEncryptedPasswords", CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public User GetUser(Guid id)
        {
            return users.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateUser(User user)
        {
            users.Add(user);
        }

        public void UpdateUser(User user)
        {
            int index = users.FindIndex(listUser => listUser.Id == user.Id);
            users[index] = user;
        }

        public void DeleteUser(Guid id)
        {
            int index = users.FindIndex(listUser => listUser.Id == id);
            users.RemoveAt(index);
        }
    }
}