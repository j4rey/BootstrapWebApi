using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteWebApi.Models;
using WebsiteWebApi.Models.DTOs;
using WebsiteWebApi.Repositories;

namespace WebsiteWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/HomeSections")]
    [EnableCors("AllowAny")]
    public class HomeSectionsController : Controller
    {
        private readonly WebSiteContext _context;

        public HomeSectionsController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/HomeSections
        [HttpGet]
        public IEnumerable<HomeSectionDTO> GetHomeSections()
        {
            return _context.HomeSections.Select(x=> new HomeSectionDTO
            {
                Id = x.Id,
                Header = x.Header,
                BackgroundImageUrl = x.BackgroundImageUrl,
                Paragraphs = x.Paragraphs,
                WebsiteId = x.WebsiteId,
                isActive = x.isActive
            }).ToList();
        }

        // GET: api/HomeSections/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var homeSection = await _context.HomeSections
                .Where(x => x.Id == id).Select(m => new HomeSectionDTO()
                {
                    Id = m.Id,
                    Header = m.Header,
                    BackgroundImageUrl = m.BackgroundImageUrl,
                    Paragraphs = m.Paragraphs,
                    WebsiteId = m.WebsiteId,
                    isActive = m.isActive
                })
                .SingleOrDefaultAsync();//(m => m.Id == id);

            if (homeSection == null)
            {
                return NotFound();
            }

            return Ok(homeSection);
        }

        // PUT: api/HomeSections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeSection([FromRoute] int id, [FromBody] HomeSection homeSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != homeSection.Id)
            {
                return BadRequest();
            }

            _context.Entry(homeSection).State = EntityState.Modified;
            //_context.Entry(homeSection.Paragraphs).State = EntityState.Modified;
            foreach(var p in homeSection.Paragraphs)
            {
                _context.Entry(p).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeSectionExists(id))
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

        // POST: api/HomeSections
        [HttpPost]
        public async Task<IActionResult> PostHomeSection([FromBody] HomeSection homeSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HomeSections.Add(homeSection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomeSection", new { id = homeSection.Id }, homeSection);
        }

        // DELETE: api/HomeSections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeSection([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var homeSection = await _context.HomeSections.SingleOrDefaultAsync(m => m.Id == id);
            if (homeSection == null)
            {
                return NotFound();
            }

            _context.HomeSections.Remove(homeSection);
            await _context.SaveChangesAsync();

            return Ok(homeSection);
        }

        private bool HomeSectionExists(int id)
        {
            return _context.HomeSections.Any(e => e.Id == id);
        }
    }
}