using System;

namespace C_Rest_chat_server.Dtos
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
