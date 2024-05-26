using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetHealthcare.Server.APIs.Controllers
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CagesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
