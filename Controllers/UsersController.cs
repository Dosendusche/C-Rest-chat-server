using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Rest_chat_server.Dtos;
using C_Rest_chat_server.Entities;
using C_Rest_chat_server.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace C_Rest_chat_server.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository repository;

        public UsersController(IUsersRepository repository)
        {
            this.repository = repository;
        }

        // GET /users
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = (await repository.GetUsersAsync()).Select(user => user.ToDto());
            return users;
        }

        // Get /users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            User user = await repository.GetUserAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user.ToDto());
        }

        // POST /users
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await repository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.ToDto());
        }

        // PATCH /users/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserAsync(Guid id, UpdateUserDto userDto)
        {
            User existingUser = await repository.GetUserAsync(id);
            if (existingUser is null)
            {
                return NotFound();
            }

            User updatedUser = existingUser with
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
            };
            await repository.UpdateUserAsync(updatedUser);

            return NoContent();
        }

        // DELETE /users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(Guid id)
        {
            User existingUser = await repository.GetUserAsync(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            await repository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}