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
    public class APIRequestMetadatasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIRequestMetadatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIRequestMetadatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIRequestMetadata>>> GetAPIRequestMetadata()
        {
            return await _context.APIRequestMetadata.ToListAsync();
        }

        // GET: api/APIRequestMetadatas/5
        [HttpGet("{RequestId}")]
        public async Task<List<APIRequestMetadata>> GetAPIRequestMetadata(Guid RequestId)
        {
            var aPIRequestMetadata = await _context.APIRequestMetadata.Where(x => x.RequestId == RequestId).ToListAsync();

            if (aPIRequestMetadata == null)
            {
                return new List<APIRequestMetadata>();
            }

            return aPIRequestMetadata;
        }

        // PUT: api/APIRequestMetadatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIRequestMetadata(int id, APIRequestMetadata aPIRequestMetadata)
        {
            if (id != aPIRequestMetadata.Id)
            {
                return BadRequest();
            }

            _context.Entry(aPIRequestMetadata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIRequestMetadataExists(id))
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

        // POST: api/APIRequestMetadatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIRequestMetadata>> PostAPIRequestMetadata(APIRequestMetadata aPIRequestMetadata)
        {
            _context.APIRequestMetadata.Add(aPIRequestMetadata);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAPIRequestMetadata", new { id = aPIRequestMetadata.Id }, aPIRequestMetadata);
        }

        // DELETE: api/APIRequestMetadatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAPIRequestMetadata(int id)
        {
            var aPIRequestMetadata = await _context.APIRequestMetadata.FindAsync(id);
            if (aPIRequestMetadata == null)
            {
                return NotFound();
            }

            _context.APIRequestMetadata.Remove(aPIRequestMetadata);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool APIRequestMetadataExists(int id)
        {
            return _context.APIRequestMetadata.Any(e => e.Id == id);
        }
    }
}
