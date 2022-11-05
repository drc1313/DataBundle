using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBundle.BL;
using DataBundle.DAL;

namespace DataBundleAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBundlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIBundlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIBundles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIBundle>>> GetAPIBundle()
        {
            return await _context.APIBundle.ToListAsync();
        }

        // GET: api/APIBundles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIBundle>> GetAPIBundle(Guid id)
        {
            var aPIBundle = await _context.APIBundle.FindAsync(id);

            if (aPIBundle == null)
            {
                return NotFound();
            }

            return aPIBundle;
        }

        // PUT: api/APIBundles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIBundle(Guid id, APIBundle aPIBundle)
        {
            if (id != aPIBundle.BundleId)
            {
                return BadRequest();
            }

            _context.Entry(aPIBundle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIBundleExists(id))
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

        // POST: api/APIBundles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIBundle>> PostAPIBundle(APIBundle aPIBundle)
        {
            _context.APIBundle.Add(aPIBundle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPIBundle", new { id = aPIBundle.BundleId }, aPIBundle);
        }

        // DELETE: api/APIBundles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIBundle(Guid id)
        {
            var aPIBundle = await _context.APIBundle.FindAsync(id);
            if (aPIBundle == null)
            {
                return NotFound();
            }

            _context.APIBundle.Remove(aPIBundle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIBundleExists(Guid id)
        {
            return _context.APIBundle.Any(e => e.BundleId == id);
        }
    }
}
