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
    [Route("api/ContactUsSections")]
    public class ContactUsSectionsController : Controller
    {
        private readonly WebSiteContext _context;

        public ContactUsSectionsController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/ContactUsSections
        [HttpGet]
        public IEnumerable<ContactUsSection> GetContactUsSections()
        {
            return _context.ContactUsSections;
        }

        // GET: api/ContactUsSections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactUsSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactUsSection = await _context.ContactUsSections.SingleOrDefaultAsync(m => m.Id == id);

            if (contactUsSection == null)
            {
                return NotFound();
            }

            return Ok(contactUsSection);
        }

        // PUT: api/ContactUsSections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactUsSection([FromRoute] int id, [FromBody] ContactUsSection contactUsSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactUsSection.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactUsSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactUsSectionExists(id))
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

        // POST: api/ContactUsSections
        [HttpPost]
        public async Task<IActionResult> PostContactUsSection([FromBody] ContactUsSection contactUsSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ContactUsSections.Add(contactUsSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactUsSection", new { id = contactUsSection.Id }, contactUsSection);
        }

        // DELETE: api/ContactUsSections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactUsSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactUsSection = await _context.ContactUsSections.SingleOrDefaultAsync(m => m.Id == id);
            if (contactUsSection == null)
            {
                return NotFound();
            }

            _context.ContactUsSections.Remove(contactUsSection);
            await _context.SaveChangesAsync();

            return Ok(contactUsSection);
        }

        private bool ContactUsSectionExists(int id)
        {
            return _context.ContactUsSections.Any(e => e.Id == id);
        }
    }
}