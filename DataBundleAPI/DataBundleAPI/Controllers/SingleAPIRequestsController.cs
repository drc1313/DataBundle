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
    public class SingleAPIRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SingleAPIRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SingleAPIRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SingleAPIRequest>>> GetAPIRequest()
        {
          if (_context.APIRequest == null)
          {
              return NotFound();
          }
            return await _context.APIRequest.ToListAsync();
        }

        // GET: api/SingleAPIRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleAPIRequest>> GetSingleAPIRequest(Guid id)
        {
          if (_context.APIRequest == null)
          {
              return NotFound();
          }
            var singleAPIRequest = await _context.APIRequest.FindAsync(id);
            var tokens = await _context.Tokens.Where(r => r.RequestId == singleAPIRequest.RequestId).ToListAsync();
            singleAPIRequest.Tokens = tokens;
            if (singleAPIRequest == null)
            {
                return NotFound();
            }

            return singleAPIRequest;
        }

        // PUT: api/SingleAPIRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSingleAPIRequest(Guid id, SingleAPIRequest singleAPIRequest)
        {
            if (id != singleAPIRequest.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(singleAPIRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SingleAPIRequestExists(id))
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

        // POST: api/SingleAPIRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SingleAPIRequest>> PostSingleAPIRequest(SingleAPIRequest singleAPIRequest)
        {
          if (_context.APIRequest == null)
          {
              return Problem("Entity set 'ApplicationDbContext.APIRequest'  is null.");
          }
            _context.APIRequest.Add(singleAPIRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSingleAPIRequest", new { id = singleAPIRequest.RequestId }, singleAPIRequest);
        }

        // DELETE: api/SingleAPIRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingleAPIRequest(Guid id)
        {
            if (_context.APIRequest == null)
            {
                return NotFound();
            }
            var singleAPIRequest = await _context.APIRequest.FindAsync(id);
            if (singleAPIRequest == null)
            {
                return NotFound();
            }

            _context.APIRequest.Remove(singleAPIRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SingleAPIRequestExists(Guid id)
        {
            return (_context.APIRequest?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}
