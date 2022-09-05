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
        public async Task<ActionResult<IEnumerable<APIAccounts>>> GetAPIAccounts()
        {
          if (_context.APIAccounts == null)
          {
              return NotFound();
          }
            return await _context.APIAccounts.ToListAsync();
        }

        // GET: api/APIAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIAccounts>> GetAPIAccounts(Guid id)
        {
          if (_context.APIAccounts == null)
          {
              return NotFound();
          }
            var aPIAccounts = await _context.APIAccounts.FindAsync(id);

            if (aPIAccounts == null)
            {
                return NotFound();
            }

            return aPIAccounts;
        }

        // PUT: api/APIAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIAccounts(Guid id, APIAccounts aPIAccounts)
        {
            if (id != aPIAccounts.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(aPIAccounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIAccountsExists(id))
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
        public async Task<ActionResult<APIAccounts>> PostAPIAccounts(APIAccounts aPIAccounts)
        {
          if (_context.APIAccounts == null)
          {
              return Problem("Entity set 'ApplicationDbContext.APIAccounts'  is null.");
          }
            _context.APIAccounts.Add(aPIAccounts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPIAccounts", new { id = aPIAccounts.AccountId }, aPIAccounts);
        }

        // DELETE: api/APIAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIAccounts(Guid id)
        {
            if (_context.APIAccounts == null)
            {
                return NotFound();
            }
            var aPIAccounts = await _context.APIAccounts.FindAsync(id);
            if (aPIAccounts == null)
            {
                return NotFound();
            }

            _context.APIAccounts.Remove(aPIAccounts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIAccountsExists(Guid id)
        {
            return (_context.APIAccounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}
