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
    [Route("api/DownloadSections")]
    public class DownloadSectionsController : Controller
    {
        private readonly WebSiteContext _context;

        public DownloadSectionsController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/DownloadSections
        [HttpGet]
        public IEnumerable<DownloadSection> GetDownloadSections()
        {
            return _context.DownloadSections;
        }

        // GET: api/DownloadSections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDownloadSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var downloadSection = await _context.DownloadSections.SingleOrDefaultAsync(m => m.Id == id);

            if (downloadSection == null)
            {
                return NotFound();
            }

            return Ok(downloadSection);
        }

        // PUT: api/DownloadSections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDownloadSection([FromRoute] int id, [FromBody] DownloadSection downloadSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != downloadSection.Id)
            {
                return BadRequest();
            }

            _context.Entry(downloadSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DownloadSectionExists(id))
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

        // POST: api/DownloadSections
        [HttpPost]
        public async Task<IActionResult> PostDownloadSection([FromBody] DownloadSection downloadSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DownloadSections.Add(downloadSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDownloadSection", new { id = downloadSection.Id }, downloadSection);
        }

        // DELETE: api/DownloadSections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDownloadSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var downloadSection = await _context.DownloadSections.SingleOrDefaultAsync(m => m.Id == id);
            if (downloadSection == null)
            {
                return NotFound();
            }

            _context.DownloadSections.Remove(downloadSection);
            await _context.SaveChangesAsync();

            return Ok(downloadSection);
        }

        private bool DownloadSectionExists(int id)
        {
            return _context.DownloadSections.Any(e => e.Id == id);
        }
    }
}