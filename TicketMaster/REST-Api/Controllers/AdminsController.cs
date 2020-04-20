using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_Api.ApiModels;

namespace REST_Api.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ITicketRepo _repo;

        public AdminsController(ITicketRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Admins>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery]string search = null)
        {
            IEnumerable<Domain.Models.Admins> notes = await _repo.GetAdminsAsync(search);

            IEnumerable<Admins> resource = notes.Select(Mapper.MapAdmins);
            return Ok(resource);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Admins), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAdminsByIdAsync(int id)
        {
            if (await _repo.GetAdminByIdAsync(id) is Domain.Models.Admins user)
            {
                Admins resource = Mapper.MapAdmins(user);
                return Ok(resource);
            }
            return NotFound("Admin doesn't exist");
        }

        // GET: api/Users/string
        [HttpGet("{email},{password}")]
        [ProducesResponseType(typeof(Admins), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyLoginAsync(string email, string password)
        {
            if (await _repo.GetAdminByLoginAsync(email, password) is Domain.Models.Admins user)
            {
                Admins resource = Mapper.MapAdmins(user);
                return Ok(resource);
            }
            return NotFound("Incorrest eamil/password. Please try again.");
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(typeof(Admins), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Admins newUser)
        {
            if (await _repo.GetAdminByEmailAsync(newUser.Email) is Domain.Models.Admins u)
            {
                return BadRequest("Email already esists");
            }
            else
            {
                var user = Mapper.MapAdmins(newUser);
                await _repo.AddAdminAsync(user);
                await _repo.SaveAsync();
                var newEntity = await _repo.GetAdminByEmailAsync(user.Email);
                return Ok(newEntity);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Admins), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Admins user)
        {
            if (await _repo.GetAdminByIdAsync(id) is Domain.Models.Admins u)
            {
                var resource = Mapper.MapAdmins(user);
                await _repo.UpdateAdminAsync(id, resource);
                await _repo.SaveAsync();
                var newEntity = await _repo.GetAdminByIdAsync(id);
                return Ok(newEntity);
            }
            return NotFound("Admin doesn't exist");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Admins), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _repo.GetAdminByIdAsync(id) is Domain.Models.Admins u)
            {
                _repo.DeleteAdminAsync(id);
                await _repo.SaveAsync();
                return Ok("Admin removed.");
            }
            return NotFound("Admin doesn't exist");
        }
    }
}