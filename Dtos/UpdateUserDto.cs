using System.ComponentModel.DataAnnotations;

namespace C_Rest_chat_server.Dtos
{
    public record UpdateUserDto
    {
        [Required(ErrorMessage = "A Name is required")]
        public string Name { get; init; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; init; }

        [Required(ErrorMessage = "A Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}