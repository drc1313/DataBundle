using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBundle.BL;
using DataBundle.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace DataBundleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBundleRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIBundleRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIBundleRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIBundleRequests>>> GetAPIBundleRequests()
        {
            return await _context.APIBundleRequests.ToListAsync();
        }

        // GET: api/APIBundleRequests/5
        [HttpGet("{BundleId}")]
        public async Task<ActionResult<IEnumerable<APIBundleRequests>>> GetAPIBundleRequests(Guid BundleId)
        {
            var aPIBundleRequests = await _context.APIBundleRequests.Where(x => x.BundleId == BundleId).ToListAsync();

            if (aPIBundleRequests == null)
            {
                return NotFound();
            }

            return aPIBundleRequests;
        }

        // PUT: api/APIBundleRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIBundleRequests(Guid id, APIBundleRequests aPIBundleRequests)
        {
            if (id != aPIBundleRequests.BundleId)
            {
                return BadRequest();
            }

            _context.Entry(aPIBundleRequests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIBundleRequestsExists(id))
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

        // POST: api/APIBundleRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIBundleRequests>> PostAPIBundleRequests(APIBundleRequests aPIBundleRequests)
        {
            _context.APIBundleRequests.Add(aPIBundleRequests);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (APIBundleRequestsExists(aPIBundleRequests.BundleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAPIBundleRequests", new { id = aPIBundleRequests.BundleId }, aPIBundleRequests);
        }

        // DELETE: api/APIBundleRequests/5
        [HttpDelete("{BundleId}/{RequestId}")]
        public async Task<IActionResult> DeleteAPIBundleRequests(Guid BundleId, Guid RequestId)
        {

            var aPIBundleRequests = await _context.APIBundleRequests.Where(x => x.BundleId == BundleId & x.RequestId == RequestId).FirstOrDefaultAsync();
            if (aPIBundleRequests == null)
            {
                return NotFound();
            }

            _context.APIBundleRequests.Remove(aPIBundleRequests);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIBundleRequestsExists(Guid id)
        {
            return _context.APIBundleRequests.Any(e => e.BundleId == id);
        }
    }
}
