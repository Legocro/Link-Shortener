using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkShortenerAPI.Models;
using LinkShortenerAPI.Services;

namespace LinkShortenerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortLinkController : ControllerBase
    {
        private readonly ShortLinkModelContext _context;
        private IShortUrl _shortUrl;
        public ShortLinkController(ShortLinkModelContext context, IShortUrl shortUrl)
        {
            _context = context;
            _shortUrl = shortUrl;
        }

        // GET: api/ShortLink
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortLinkModel>>> GetShortLinks()
        {
          if (_context.ShortLinks == null)
          {
              return NotFound();
          }
            return await _context.ShortLinks.ToListAsync();
        }

        // GET: api/ShortLink/5
        [HttpGet("{shortLink}")]
        public async Task<ActionResult<ShortLinkModel>> GetRedirect(string shortLink)
        {
          if (_context.ShortLinks == null)
          {
              return NotFound();
          }

            var shortLinkModel = await _context.ShortLinks.FirstOrDefaultAsync(model => model.ShortLink == shortLink);
            if (shortLinkModel == null)
            {
                return NotFound();
            }

            return Redirect(shortLinkModel.LongLink);
        }

        // PUT: api/ShortLink/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShortLinkModel(int id, ShortLinkModel shortLinkModel)
        {
            if (id != shortLinkModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(shortLinkModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShortLinkModelExists(id))
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

        // POST: api/ShortLink
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShortLinkModel>> PostShortLinkModel(ShortLinkModel shortLinkModel)
        {
          if (_context.ShortLinks == null)
          {
              return Problem("Entity set 'ShortLinkModelContext.ShortLinks'  is null.");
          }
            long epochTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            shortLinkModel.ShortLink = _shortUrl.Encode(epochTimestamp);
            shortLinkModel.CreationDate = epochTimestamp.ToString();
            _context.ShortLinks.Add(shortLinkModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRedirect", new { shortLink = shortLinkModel.ShortLink }, shortLinkModel);
        }

        // DELETE: api/ShortLink/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortLinkModel(int id)
        {
            if (_context.ShortLinks == null)
            {
                return NotFound();
            }
            var shortLinkModel = await _context.ShortLinks.FindAsync(id);
            if (shortLinkModel == null)
            {
                return NotFound();
            }

            _context.ShortLinks.Remove(shortLinkModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShortLinkModelExists(int id)
        {
            return (_context.ShortLinks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
