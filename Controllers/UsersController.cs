using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<UserDto> GetUsers()
        {
            var users = repository.GetUsers().Select(user => user.ToDto());
            return users;
        }

        // Get /users/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            User user = repository.GetUser(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user.ToDto());
        }

        // POST /users
        [HttpPost]
        public ActionResult<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.ToDto());
        }

        // PATCH /users/{id}
        [HttpPatch("{id}")]
        public ActionResult UpdateUser(Guid id, UpdateUserDto userDto)
        {
            User existingUser = repository.GetUser(id);
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
            repository.UpdateUser(updatedUser);

            return NoContent();
        }

        // DELETE /users/{id}
        [HttpDelete]
        public ActionResult DeleteUser(Guid id)
        {
            User existingUser = repository.GetUser(id);
            if (existingUser is null)
            {
                return NotFound();
            }
            repository.DeleteUser(id);

            return NoContent();
        }
    }
}