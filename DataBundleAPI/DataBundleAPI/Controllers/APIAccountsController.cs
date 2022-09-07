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
        [HttpGet("{id}")]
        public async Task<ActionResult<APIAccount>> GetAPIAccount(Guid id)
        {
          if (_context.APIAccount == null)
          {
              return NotFound();
          }
            var aPIAccount = await _context.APIAccount.FindAsync(id);

            if (aPIAccount == null)
            {
                return NotFound();
            }

            return aPIAccount;
        }

        // PUT: api/APIAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIAccount(Guid id, APIAccount aPIAccount)
        {
            aPIAccount.AccountId = id;
            if (id != aPIAccount.AccountId)
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
                if (!APIAccountExists(id))
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
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPIAccount", new { id = aPIAccount.AccountId }, aPIAccount);
        }

        // DELETE: api/APIAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIAccount(Guid id)
        {
            if (_context.APIAccount == null)
            {
                return NotFound();
            }
            var aPIAccount = await _context.APIAccount.FindAsync(id);
            if (aPIAccount == null)
            {
                return NotFound();
            }

            _context.APIAccount.Remove(aPIAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIAccountExists(Guid id)
        {
            return (_context.APIAccount?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
