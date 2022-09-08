using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBundle.BL;
using DataBundle.DAL;

namespace DataBundleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIAccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIAccount>>> GetAPIAccount()
        {
          if (_context.APIAccount == null)
          {
              return NotFound();
          }
            return await _context.APIAccount.ToListAsync();
        }

        // GET: api/APIAccounts/5
        [HttpGet("{AccountName}")]
        public async Task<ActionResult<APIAccount>> GetAPIAccount(string AccountName)
        {
          if (_context.APIAccount == null)
          {
              return NotFound();
          }
            var aPIAccount = await _context.APIAccount.FindAsync(AccountName);

            if (aPIAccount == null)
            {
                return NotFound();
            }

            return aPIAccount;
        }

        // PUT: api/APIAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{AccountName}")]
        public async Task<IActionResult> PutAPIAccount(string AccountName, APIAccount aPIAccount)
        {
            if (AccountName != aPIAccount.AccountName)
            {
                return BadRequest();
            }

            _context.Entry(aPIAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIAccountExists(AccountName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/APIAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIAccount>> PostAPIAccount(APIAccount aPIAccount)
        {
          if (_context.APIAccount == null)
          {
              return Problem("Entity set 'ApplicationDbContext.APIAccount'  is null.");
          }
            _context.APIAccount.Add(aPIAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (APIAccountExists(aPIAccount.AccountName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAPIAccount", new { id = aPIAccount.AccountName }, aPIAccount);
        }

        // DELETE: api/APIAccounts/5
        [HttpDelete("{AccountName}")]
        public async Task<IActionResult> DeleteAPIAccount(string AccountName)
        {
            if (_context.APIAccount == null)
            {
                return NotFound();
            }
            var aPIAccount = await _context.APIAccount.FindAsync(AccountName);
            if (aPIAccount == null)
            {
                return NotFound();
            }

            _context.APIAccount.Remove(aPIAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIAccountExists(string AccountName)
        {
            return (_context.APIAccount?.Any(e => e.AccountName == AccountName)).GetValueOrDefault();
        }
    }
}
