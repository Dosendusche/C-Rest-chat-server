using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C_Rest_chat_server.Entities;

namespace C_Rest_chat_server.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}
