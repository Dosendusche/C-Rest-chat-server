using System;
using System.Collections.Generic;
using C_Rest_chat_server.Entities;

namespace C_Rest_chat_server.Repositories
{
    public interface IUsersRepository
    {
        User GetUser(Guid id);
        IEnumerable<User> GetUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
    }
}