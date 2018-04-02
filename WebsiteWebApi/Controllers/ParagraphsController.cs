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
    [Route("api/Paragraphs")]
    public class ParagraphsController : Controller
    {
        private readonly WebSiteContext _context;

        public ParagraphsController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/Paragraphs
        [HttpGet]
        public IEnumerable<Paragraph> GetParagraphs()
        {
            return _context.Paragraphs;
        }

        // GET: api/Paragraphs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParagraph([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paragraph = await _context.Paragraphs.SingleOrDefaultAsync(m => m.Id == id);

            if (paragraph == null)
            {
                return NotFound();
            }

            return Ok(paragraph);
        }

        // PUT: api/Paragraphs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParagraph([FromRoute] int id, [FromBody] Paragraph paragraph)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paragraph.Id)
            {
                return BadRequest();
            }

            _context.Entry(paragraph).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParagraphExists(id))
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

        // POST: api/Paragraphs
        [HttpPost]
        public async Task<IActionResult> PostParagraph([FromBody] Paragraph paragraph)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Paragraphs.Add(paragraph);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParagraph", new { id = paragraph.Id }, paragraph);
        }

        // DELETE: api/Paragraphs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParagraph([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paragraph = await _context.Paragraphs.SingleOrDefaultAsync(m => m.Id == id);
            if (paragraph == null)
            {
                return NotFound();
            }

            _context.Paragraphs.Remove(paragraph);
            await _context.SaveChangesAsync();

            return Ok(paragraph);
        }

        private bool ParagraphExists(int id)
        {
            return _context.Paragraphs.Any(e => e.Id == id);
        }
    }
}