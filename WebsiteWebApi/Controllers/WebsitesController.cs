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
    [Route("api/Websites")]
    [EnableCors("AllowAny")]
    public class WebsitesController : Controller
    {
        private readonly WebSiteContext _context;

        public WebsitesController(WebSiteContext context)
        {
            _context = context;
        }

        // GET: api/Websites
        [HttpGet]
        public
            IEnumerable<Website> 
            //string
            GetWebsites()
        {
            var v = _context.Websites.Select(x => new Website
            {
                Id = x.Id,
                WebsiteUrl = x.WebsiteUrl,
                Name = x.Name,

                home = new HomeSection() {
                    Id = x.home.Id,
                    Header = x.home.Header,
                    BackgroundImageUrl = x.home.BackgroundImageUrl,
                    WebsiteId = x.Id,
                    Paragraphs = x.home.Paragraphs,
                    isActive= x.home.isActive
                }
                //HomeHeader = x.home.Header,
                //HomeBackgroundImageUrl = x.home.BackgroundImageUrl,
                //HomeParagraphs = x.home.Paragraphs.Select(y=>y.Text).ToList()
            }).ToList();
            return v;
        }

        // GET: api/Websites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebsite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var website = await _context.Websites.SingleOrDefaultAsync(m => m.Id == id);
            var website = await _context.Websites
                .Where(m => m.Id == id).Select(x => new WebsitesDTO()
            {
                website = new WebsiteDTO() {
                    Id = x.Id,
                    Name = x.Name,
                    WebsiteUrl = x.WebsiteUrl
                },
                Home = new HomeSectionDTO() {
                    Id = x.home.Id,
                    Header = x.home.Header,
                    BackgroundImageUrl = x.home.BackgroundImageUrl,
                    Paragraphs = x.home.Paragraphs,
                    WebsiteId = x.home.WebsiteId,
                    isActive = x.home.isActive
                }

            }).SingleOrDefaultAsync();

            //var website = Task.Run(() =>
            //{
            //    return _context.Websites.Where(m => m.Id == id).Select(x => new WebsitesDTO()
            //    {
            //        website = new WebsiteDTO() { Id = x.Id, Name = x.Name, WebsiteUrl = x.WebsiteUrl },
            //        home = new HomeSection() { Id = x.home.Id, Header = x.home.Header, BackgroundImageUrl = x.home.BackgroundImageUrl }
            //    });
            //});

            if (website == null)
            {
                return NotFound();
            }

            return Ok(website);
        }

        // PUT: api/Websites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebsite([FromRoute] int id, [FromBody] Website website)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != website.Id)
            {
                return BadRequest();
            }

            _context.Entry(website).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebsiteExists(id))
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

        // POST: api/Websites
        [HttpPost]
        public async Task<IActionResult> PostWebsite([FromBody] Website website)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Websites.Add(website);
            Website newWebsite = new Website() { Id=website.Id, Name = website.Name, WebsiteUrl = website.WebsiteUrl};

            HomeSection newhome = new HomeSection()
            {
                Header = website.Name + "Head",
                // BackgroundImageUrl = website.Name + "bg url",
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph(){Text =  "Home Para 1"},
                    new Paragraph(){Text =  "Home Para 2" }
                },
                WebsiteId = website.Id,
                isActive = true
            };
            AboutSection newabout = new AboutSection()
            {
                Header = website.Name + "about",
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph(){Text =  "about Para 1"},
                    new Paragraph(){Text =  "about Para 2" }
                },
                WebsiteId = website.Id,
                isActive = true
            };
            _context.HomeSections.Add(newhome);
            //website.home = newhome;
            await _context.SaveChangesAsync();

            CreatedAtActionResult car = CreatedAtAction("GetWebsite", new { id = newWebsite.Id }, newWebsite);
            return car;
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //_context.Websites.Add(website);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction("GetWebsite", new { id = website.Id }, website);
        }

        // DELETE: api/Websites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var website = await _context.Websites.SingleOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            _context.Websites.Remove(website);
            await _context.SaveChangesAsync();

            return Ok(website);
        }

        private bool WebsiteExists(int id)
        {
            return _context.Websites.Any(e => e.Id == id);
        }
    }
}