using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using REST_Api.ApiModels;

namespace REST_Api.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepo _repo;

        public TicketsController(ITicketRepo repo)
        {
            _repo = repo;
        }

        // GET: api/tickets
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Tickets>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery]string search = null)
        {
            IEnumerable<Domain.Models.Tickets> tickets = await _repo.GetTicketsAsync(search);

            IEnumerable<Tickets> resource = tickets.Select(Mapper.MapTickets);
            return Ok(resource);
        }


        // GET: api/Users/5
        [HttpGet("{searchType},{id}")]
        [ProducesResponseType(typeof(Tickets), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(string searchType, int id)
        {

            if (searchType.ToLower() == "admin")
            {
                if (await _repo.GetAdminByIdAsync(id) is Domain.Models.Admins admin)
                {
                    if (await _repo.GetTicketsByAdminAsync(id) is IEnumerable<Domain.Models.Tickets> tickets)
                    {
                        if (tickets.Count() == 0)
                        {
                            return NotFound("Admin has no tickets");
                        }
                        else
                        {
                            IEnumerable<Tickets> resource = tickets.Select(Mapper.MapTickets);
                            return Ok(resource);
                        }
                    }
                    return NotFound("dunno wha happen");
                }
                return BadRequest("Incorrect Id. Admin does not exist.");
            }

            else if (searchType.ToLower() == "store")
            {
                if (await _repo.GetStoreByIdAsync(id) is Domain.Models.Stores store)
                {
                    if (await _repo.GetTicketsByStoreAsync(id) is IEnumerable<Domain.Models.Tickets> tickets)
                    {
                        if (tickets.Count() == 0)
                        {
                            return NotFound("Store has no tickets");
                        }
                        else
                        {
                            IEnumerable<Tickets> resource = tickets.Select(Mapper.MapTickets);
                            return Ok(resource);
                        }
                    }
                    return NotFound("dunno wha happen");
                }
                return BadRequest("Incorrect Id. Store does not exist.");
            }

            else if (searchType.ToLower() == "user")
            {
                if (await _repo.GetUserByIdAsync(id) is Domain.Models.Users user)
                {
                    if (await _repo.GetTicketsByUserAsync(id) is IEnumerable<Domain.Models.Tickets> tickets)
                    {
                        if (tickets.Count() == 0)
                        {
                            return NotFound("User has no tickets");
                        }
                        else
                        {
                            IEnumerable<Tickets> resource = tickets.Select(Mapper.MapTickets);
                            return Ok(resource);
                        }
                    }
                    return NotFound("dunno wha happen");
                }
                return BadRequest("Incorrect Id. User does not exist.");
            }

            else
            {
                return BadRequest("Incorrect search type. Please enter 'admin', 'store', or 'user'.");
            }
        }


        //// POST: api/Users
        //[HttpPost]
        //[ProducesResponseType(typeof(Admins), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PostAsync(Admins newUser)
        //{
        //    if (await _repo.GetAdminByEmailAsync(newUser.Email) is Domain.Models.Admins u)
        //    {
        //        return BadRequest("Email already esists");
        //    }
        //    else
        //    {
        //        var user = Mapper.MapAdmins(newUser);
        //        await _repo.AddAdminAsync(user);
        //        await _repo.SaveAsync();
        //        var newEntity = await _repo.GetAdminByEmailAsync(user.Email);
        //        return Ok(newEntity);
        //    }
        //}

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(Admins), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PutAsync(int id, [FromBody] Admins user)
        //{
        //    if (await _repo.GetAdminByIdAsync(id) is Domain.Models.Admins u)
        //    {
        //        var resource = Mapper.MapAdmins(user);
        //        await _repo.UpdateAdminAsync(id, resource);
        //        await _repo.SaveAsync();
        //        var newEntity = await _repo.GetAdminByIdAsync(id);
        //        return Ok(newEntity);
        //    }
        //    return NotFound("Admin doesn't exist");
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Admins), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _repo.GetTicketByIdAsync(id) is Domain.Models.Tickets t)
            {
                _repo.DeleteTicketAsync(id);
                if (await _repo.GetTicketByIdAsync(id) is Domain.Models.Tickets tick)
                {
                    return StatusCode(500, "Uknown error, ticket not deleted.");
                }
                else if (await _repo.GetTicketByIdAsync(id) is null)
                {
                    await _repo.SaveAsync();
                    return Ok("Ticket removed.");
                }
            }
            return NotFound("Ticket doesn't exist");
        }
    }
}