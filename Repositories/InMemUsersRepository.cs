using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await Task.FromResult(users);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = users.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(user);
        }

        public async Task CreateUserAsync(User user)
        {
            users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateUserAsync(User user)
        {
            int index = users.FindIndex(listUser => listUser.Id == user.Id);
            users[index] = user;
            await Task.CompletedTask;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            int index = users.FindIndex(listUser => listUser.Id == id);
            users.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}