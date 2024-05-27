using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.Models;
using PetHealthcare.Server.Services.Interfaces;

namespace PetHealthcare.Server.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _context;

        public AccountsController(IAccountService context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public IEnumerable<Account> GetAccounts()
        {
            return _context.GetAllAccounts();
        }

        [HttpGet("/api/byRole/{role}")]
        public IEnumerable<Account> GetAllAccountsByRole([FromBody] int role)
        {
            return null;
        }

        [HttpGet("/api/pets/{id}")]
        public IEnumerable<Pet> GetAccountPets([FromRoute] string id)
        {
            var account = _context.GetAccountByCondition(a => a.AccountId == id);
            var list = _context.GetAccountPets(account);
            return list; 
        }

        [HttpGet("/api/byRole/{role}&{id}")]
        public Account GetAccountByRole([FromRoute] string role, [FromRoute] string id)
        {
            return null; 
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount([FromRoute] string id)
        {
            var account = _context.GetAccountByCondition(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, AccountDTO account)
        {
            _context.UpdateAccount(id, account);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AccountExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount([FromBody] AccountDTO accountDTO)
        {
            try
            {
                _context.CreateAccount(accountDTO);
            } 
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateException)
            //{
            //    if (AccountExists(account.AccountId))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtAction("GetAccount", new { id = accountDTO.GetHashCode() }, accountDTO);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = _context.GetAccountByCondition(a => a.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            _context.DeleteAccount(account);

            return NoContent();
        }
    }
}
