using Microsoft.AspNetCore.Mvc;
using PetHealthcareSystem.APIs.DTOS;
using PetHealthcareSystem.Models;
using PetHealthcareSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetHealthcareSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CagesController : ControllerBase
    {
        private readonly ICageService _context;

        public CagesController(ICageService context)
        {
            _context = context;
        }

        // GET: api/<CagesController>
        [HttpGet]
        public IEnumerable<Cage> GetCages()
        {
            return _context.GetAllCages();
        }

        // GET api/<CagesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cage>> GetCage([FromRoute] int id)
        {
            var cage = _context.GetCageByCondition(a => a.CageId == id);

            if (cage == null)
            {
                return NotFound();
            }

            return cage;
        }

        // POST api/<CagesController>
        [HttpPost]
        public ActionResult Post([FromBody] CageDTO newCage)
        {
            _context.CreateCage(newCage);

            return CreatedAtAction(nameof(Post), newCage.GetHashCode(), newCage);
        }

        // PUT api/<CagesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] bool isOccupied)
        {
            var cage = _context.GetCageByCondition(c => c.CageId == id);
            if (cage == null)
            {
                return BadRequest("No such cage");
            }
            _context.UpdateCage(id, new DTOS.CageDTO { IsOccupied = isOccupied} );

            return Ok();
        }

        // DELETE api/<CagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
