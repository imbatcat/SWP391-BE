using Microsoft.AspNetCore.Mvc;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _context;

        public RolesController(IRoleService context)
        {
            _context = context;
        }


        // GET: api/Roles
        [HttpGet]
        public async Task<IEnumerable<Role>> GetRole()
        {
            return await _context.GetAllRole();
        }

        //// GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleByCondition(int id)
        {
            var role = await _context.GetRoleByCondition(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        //// PUT: api/Roles/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id, [FromBody] RoleDTO toUpdateRole)
        {
            var role = await _context.GetRoleByCondition(r => r.RoleId == id);
            if (role == null)
            {
                return BadRequest();
            }
            await _context.UpdateRole(id, toUpdateRole);
            return Ok(toUpdateRole);
        }

        //// POST: api/Roles
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> CreateService([FromBody] RoleDTO toCreateRole)
        {
            await _context.CreateRole(toCreateRole);

            return Ok(toCreateRole);
        }

        //// DELETE: api/Roles/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRole(int id)
        //{
        //    var role = await _context.Roles.FindAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Roles.Remove(role);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RoleExists(int id)
        //{
        //    return _context.Roles.Any(e => e.RoleId == id);
        //}
    }
}
