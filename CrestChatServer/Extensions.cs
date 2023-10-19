using C_Rest_chat_server.Dtos;
using C_Rest_chat_server.Entities;

namespace C_Rest_chat_server
{
    public static class Extensions
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = user.CreatedDate
            };
        }
    }
}
