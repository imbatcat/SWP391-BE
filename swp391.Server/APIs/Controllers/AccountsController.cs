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
        private readonly IPetService _contextPet;

        public AccountsController(IAccountService context, IPetService contextPet)
        {
            _context = context;
            _contextPet = contextPet;
        }

        // GET: api/Accounts
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _context.GetAllAccounts();
        }

        [HttpGet("/api/byRole/{roleId}")]
        public async Task<IEnumerable<Account>> GetAllAccountsByRole([FromRoute] int roleId)
        {
            return await _context.GetAllAccountsByRole(roleId);
        }

        [HttpGet("/api/byRole/{roleId}&{id}")]
        public async Task<ActionResult<Account>> GetAccountByRole([FromRoute] string roleId, [FromRoute] string id)
        {
            var checkAccount = await _context.GetAccountByCondition(a => a.AccountId == id);
            if (checkAccount == null)
            {
                return NotFound();
            }
            return Ok(checkAccount);
        }

        //[HttpGet("/api/account/pets/{id}")]
        //public IEnumerable<Pet> GetAccountPets([FromRoute] string id)
        //{
        //    var checkAccount = _context.GetAccountByCondition(a => a.AccountId == id);
        //    if (checkAccount == null)
        //    {
        //    }
        //    return _context.GetAccountPets(checkAccount);
        //}

        [HttpGet("/api/accounts/pets/{id}")]
        public async Task<IEnumerable<Pet>> GetAccountPets([FromRoute] string id)
        {
            return await _contextPet.GetAccountPets(id);

        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount([FromRoute] string id)
        {
            var account = await _context.GetAccountByCondition(a => a.AccountId == id);

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
            await _context.UpdateAccount(id, account);

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
                await _context.CreateAccount(accountDTO);
            } catch (BadHttpRequestException ex)
            {
                return (BadRequest(ex.Message));
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

        [HttpPost("/api/accounts/login")]
        
        public async Task<ActionResult<Account>> LoginAccount([FromBody] string username, [FromBody] string password)
        {
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
            var account = await _context.GetAccountByCondition(a => a.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            _context.DeleteAccount(account);

            return NoContent();
        }
    }
}
