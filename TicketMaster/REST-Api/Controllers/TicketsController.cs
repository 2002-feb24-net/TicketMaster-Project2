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
    }
}