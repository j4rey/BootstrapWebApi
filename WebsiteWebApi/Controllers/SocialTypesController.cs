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
    [Route("api/SocialTypes")]
    [Microsoft.AspNetCore.Cors.EnableCors("AllowAny")]
    public class SocialTypesController : Controller
    {
        private readonly WebSiteContext _context;

        public SocialTypesController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/SocialTypes
        [HttpGet]
        public IEnumerable<SocialType> GetSocialTypes()
        {
            return _context.SocialTypes;
        }

        // GET: api/SocialTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socialType = await _context.SocialTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (socialType == null)
            {
                return NotFound();
            }

            return Ok(socialType);
        }

        // PUT: api/SocialTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocialType([FromRoute] int id, [FromBody] SocialType socialType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialType.Id)
            {
                return BadRequest();
            }

            _context.Entry(socialType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialTypeExists(id))
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

        // POST: api/SocialTypes
        [HttpPost]
        public async Task<IActionResult> PostSocialType([FromBody] SocialType socialType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SocialTypes.Add(socialType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocialType", new { id = socialType.Id }, socialType);
        }

        // DELETE: api/SocialTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socialType = await _context.SocialTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (socialType == null)
            {
                return NotFound();
            }

            _context.SocialTypes.Remove(socialType);
            await _context.SaveChangesAsync();

            return Ok(socialType);
        }

        private bool SocialTypeExists(int id)
        {
            return _context.SocialTypes.Any(e => e.Id == id);
        }
    }
}