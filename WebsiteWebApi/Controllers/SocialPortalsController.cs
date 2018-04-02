using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteWebApi.Models;
using WebsiteWebApi.Repositories;

namespace WebsiteWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/SocialPortals")]
    public class SocialPortalsController : Controller
    {
        private readonly WebSiteContext _context;

        public SocialPortalsController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/SocialPortals
        [HttpGet]
        public IEnumerable<SocialPortal> GetSocialPortals()
        {
            return _context.SocialPortals;
        }

        // GET: api/SocialPortals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialPortal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socialPortal = await _context.SocialPortals.SingleOrDefaultAsync(m => m.Id == id);

            if (socialPortal == null)
            {
                return NotFound();
            }

            return Ok(socialPortal);
        }

        // PUT: api/SocialPortals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocialPortal([FromRoute] int id, [FromBody] SocialPortal socialPortal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialPortal.Id)
            {
                return BadRequest();
            }

            _context.Entry(socialPortal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialPortalExists(id))
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

        // POST: api/SocialPortals
        [HttpPost]
        public async Task<IActionResult> PostSocialPortal([FromBody] SocialPortal socialPortal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SocialPortals.Add(socialPortal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocialPortal", new { id = socialPortal.Id }, socialPortal);
        }

        // DELETE: api/SocialPortals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialPortal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socialPortal = await _context.SocialPortals.SingleOrDefaultAsync(m => m.Id == id);
            if (socialPortal == null)
            {
                return NotFound();
            }

            _context.SocialPortals.Remove(socialPortal);
            await _context.SaveChangesAsync();

            return Ok(socialPortal);
        }

        private bool SocialPortalExists(int id)
        {
            return _context.SocialPortals.Any(e => e.Id == id);
        }
    }
}