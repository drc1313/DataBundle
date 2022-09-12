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
    public class APIUsagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIUsagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIUsages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIUsage>>> GetAPIUsage()
        {
          if (_context.APIUsage == null)
          {
              return NotFound();
          }
            return await _context.APIUsage.ToListAsync();
        }

        // GET: api/APIUsages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIUsage>> GetAPIUsage(Guid id)
        {
          if (_context.APIUsage == null)
          {
              return NotFound();
          }
            var aPIUsage = await _context.APIUsage.FindAsync(id);

            if (aPIUsage == null)
            {
                return NotFound();
            }

            return aPIUsage;
        }

        // PUT: api/APIUsages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIUsage(Guid id, APIUsage aPIUsage)
        {
            if (id != aPIUsage.UsageID)
            {
                return BadRequest();
            }

            _context.Entry(aPIUsage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIUsageExists(id))
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

        // POST: api/APIUsages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIUsage>> PostAPIUsage(APIUsage aPIUsage)
        {
          if (_context.APIUsage == null)
          {
              return Problem("Entity set 'ApplicationDbContext.APIUsage'  is null.");
          }
            _context.APIUsage.Add(aPIUsage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPIUsage", new { id = aPIUsage.UsageID }, aPIUsage);
        }

        // DELETE: api/APIUsages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIUsage(Guid id)
        {
            if (_context.APIUsage == null)
            {
                return NotFound();
            }
            var aPIUsage = await _context.APIUsage.FindAsync(id);
            if (aPIUsage == null)
            {
                return NotFound();
            }

            _context.APIUsage.Remove(aPIUsage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIUsageExists(Guid id)
        {
            return (_context.APIUsage?.Any(e => e.UsageID == id)).GetValueOrDefault();
        }
    }
}
