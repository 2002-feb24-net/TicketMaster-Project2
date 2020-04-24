using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_Api.ApiModels;

namespace REST_Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITicketRepo _repo;

        public UsersController(ITicketRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Users>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery]string lastName = null)
        {
            IEnumerable<Domain.Models.Users> notes = await _repo.GetUsersAsync(lastName);

            IEnumerable<Users> resource = notes.Select(Mapper.MapUsers);
            return Ok(resource);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (await _repo.GetUserByIdAsync(id) is Domain.Models.Users user)
            {
                Users resource = Mapper.MapUsers(user);
                return Ok(resource);
            }
            return NotFound("User doesn't exist");
        }

        // GET: api/Users/string
        [HttpGet("{email},{password}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyLoginAsync(string email, string password)
        {
            if (await _repo.GetUserByLoginAsync(email, password) is Domain.Models.Users user)
            {
                Users resource = Mapper.MapUsers(user);
                return Ok(resource);
            }
            return NotFound("Incorrect email/password. Please try again.");
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Users newUser)
        {
            if (await _repo.GetUserByEmailAsync(newUser.Email) is Domain.Models.Users u)
            {
                return BadRequest("Email already esists");
            }
            else
            {
                var user = Mapper.MapUsers(newUser);
                _repo.AddUserAsync(user);
                await _repo.SaveAsync();
                var newEntity = await _repo.GetUserByEmailAsync(user.Email);
                return Ok(newEntity);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Users user)
        {
            if (await _repo.GetUserByIdAsync(id) is Domain.Models.Users u)
            {
                var resource = Mapper.MapUsers(user);
                await _repo.UpdateUserAsync(id, resource);
                await _repo.SaveAsync();
                var newEntity = await _repo.GetUserByIdAsync(id);
                return Ok(newEntity);
            }
            return NotFound("User doesn't exist");
        }

        // PUT: api/Users/int,string
        [HttpPut("{userId},{password}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutToChangePasswordAsync(int userId, string password)
        {
            if (await _repo.GetUserByIdAsync(userId) is Domain.Models.Users)
            {
                var newEntity = await _repo.UpdateUserPasswordAsync(userId, password);
                await _repo.SaveAsync();
                return Ok(newEntity);
            }
            else
                return NotFound("User id does not exist");
        }

        // DELETE: api/users/int
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _repo.GetUserByIdAsync(id) is Domain.Models.Users u)
            {
                _repo.DeleteUserAsync(id);
                await _repo.SaveAsync();
                return Ok("Admin removed.");
            }
            return NotFound("User doesn't exist");
        }
    }
}
