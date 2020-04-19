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
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAsync([FromQuery]string search = null)
        {
            IEnumerable<Domain.Models.Users> notes = await _repo.GetUsersAsync(search);

            IEnumerable<Users> resource = notes.Select(Mapper.MapUsers);
            return Ok(resource);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyIdAsync(int id)
        {
            if (await _repo.GetUserByIdAsync(id) is Domain.Models.Users user)
            {
                Users resource = Mapper.MapUsers(user);
                return Ok(resource);
            }
            return NotFound();
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async void /*Task<IActionResult>*/ PostAsync(Users newUser)
        {
            //if (await _repo.GetUserByIdAsync(newNote.AuthorId) is Core.User author)
            //{
            //    var note = new Core.Note
            //    {
            //        Author = author,
            //        Text = newNote.Text,
            //        Tags = newNote.Tags
            //    };

            //    Core.Note result = await _noteRepository.AddNoteAsync(note);

            //    Note resource = Mapper.MapNote(result);
            //    return CreatedAtAction(nameof(GetByIdAsync), new { id = note.Id }, resource);
            //}

            //ModelState.AddModelError(nameof(newNote.AuthorId), "User does not exist");
            //return BadRequest(ModelState);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
