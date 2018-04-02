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
    [Route("api/AboutSections")]
    public class AboutSectionsController : Controller
    {
        private readonly WebSiteContext _context;

        public AboutSectionsController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/AboutSections
        [HttpGet]
        public IEnumerable<AboutSection> GetAboutSections()
        {
            return _context.AboutSections;
        }

        // GET: api/AboutSections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aboutSection = await _context.AboutSections.SingleOrDefaultAsync(m => m.Id == id);

            if (aboutSection == null)
            {
                return NotFound();
            }

            return Ok(aboutSection);
        }

        // PUT: api/AboutSections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAboutSection([FromRoute] int id, [FromBody] AboutSection aboutSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aboutSection.Id)
            {
                return BadRequest();
            }

            _context.Entry(aboutSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutSectionExists(id))
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

        // POST: api/AboutSections
        [HttpPost]
        public async Task<IActionResult> PostAboutSection([FromBody] AboutSection aboutSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AboutSections.Add(aboutSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAboutSection", new { id = aboutSection.Id }, aboutSection);
        }

        // DELETE: api/AboutSections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAboutSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aboutSection = await _context.AboutSections.SingleOrDefaultAsync(m => m.Id == id);
            if (aboutSection == null)
            {
                return NotFound();
            }

            _context.AboutSections.Remove(aboutSection);
            await _context.SaveChangesAsync();

            return Ok(aboutSection);
        }

        private bool AboutSectionExists(int id)
        {
            return _context.AboutSections.Any(e => e.Id == id);
        }
    }
}