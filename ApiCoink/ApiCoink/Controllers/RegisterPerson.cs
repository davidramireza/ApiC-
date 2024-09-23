using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiCoink.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterPersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public RegisterPersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/RegisterPerson/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterPerson>> Details(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST: api/RegisterPerson/create
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] RegisterPerson person)
        {
            if (ModelState.IsValid)
            {
                await _personService.AddAsync(person);
                return CreatedAtAction(nameof(Details), new { id = person.Id }, person);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/RegisterPerson/edit/{id}
        [HttpPut("edit/{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] RegisterPerson person)
        {
            if (id != person.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existingPerson = await _personService.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound();
            }

            await _personService.UpdateAsync(person);
            return NoContent();
        }

        // DELETE: api/RegisterPerson/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personService.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("all")]
        public async Task<ActionResult<List<RegisterPerson>>> GetAllPersons()
        {
            var persons = await _personService.GetAllAsync();

            if (persons == null || !persons.Any())
            {
                return NotFound("No hay personas registradas.");
            }

            return Ok(persons);
        }
    }
}

