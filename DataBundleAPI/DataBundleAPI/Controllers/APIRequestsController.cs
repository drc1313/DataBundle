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
    public class APIRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIRequest>>> GetAPIRequest()
        {
            return await _context.APIRequest.ToListAsync();
        }

        // GET: api/APIRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIRequest>> GetAPIRequest(Guid id)
        {
            var aPIRequest = await _context.APIRequest.FindAsync(id);

            if (aPIRequest == null)
            {
                return NotFound();
            }

            return aPIRequest;
        }

        // PUT: api/APIRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIRequest(Guid id, APIRequest aPIRequest)
        {
            aPIRequest.RequestId = id;
            if (id != aPIRequest.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(aPIRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIRequestExists(id))
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

        // POST: api/APIRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIRequest>> PostAPIRequest(APIRequest aPIRequest)
        {
            _context.APIRequest.Add(aPIRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPIRequest", new { id = aPIRequest.RequestId }, aPIRequest);
        }

        // DELETE: api/APIRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIRequest(Guid id)
        {
            var aPIRequest = await _context.APIRequest.FindAsync(id);
            if (aPIRequest == null)
            {
                return NotFound();
            }

            _context.APIRequest.Remove(aPIRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIRequestExists(Guid id)
        {
            return _context.APIRequest.Any(e => e.RequestId == id);
        }
    }
}
