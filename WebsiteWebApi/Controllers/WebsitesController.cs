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
                    },
                    about = new AboutSectionDTO()
                    {
                        Id = x.about.Id,
                        Header = x.about.Header,
                        BackgroundImageUrl = x.about.BackgroundImageUrl,
                        Paragraphs = x.about.Paragraphs,
                        WebsiteId = x.about.WebsiteId,
                        isActive = x.about.isActive
                    },
                    download = new DownloadSectionDTO()
                    {
                        Id = x.download.Id,
                        Header = x.download.Header,
                        BackgroundImageUrl = x.download.BackgroundImageUrl,
                        Paragraphs = x.download.Paragraphs,
                        WebsiteId = x.download.WebsiteId,
                        isActive = x.download.isActive
                    },
                    contactus = new ContactUsSectionDTO()
                    {
                        Id = x.contactus.Id,
                        Header = x.contactus.Header,
                        BackgroundImageUrl = x.contactus.BackgroundImageUrl,
                        Paragraphs = x.contactus.Paragraphs,
                        WebsiteId = x.contactus.WebsiteId,
                        isActive = x.contactus.isActive,
                        SocialPortals = x.contactus.SocialPortals.Select(s => new SocialPortalDTO
                        {
                            Id = s.Id,
                            url = s.url,
                            ContactUsId = s.ContactUsId,
                            socialtype = new SocialTypeDTO()
                            {
                                Id = s.socialtype.Id,
                                Title = s.socialtype.Title,
                                CSS = s.socialtype.CSS,
                                SocialPortalId = s.socialtype.SocialPortalId
                            }
                        }).ToList()
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
            _context.HomeSections.Add(newhome);
            AboutSection newabout = new AboutSection()
            {
                Header = website.Name + " about",
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph(){Text =  "about Para 1"},
                    new Paragraph(){Text =  "about Para 2" }
                },
                WebsiteId = website.Id,
                isActive = true
            };
            _context.AboutSections.Add(newabout);
            DownloadSection newdownload = new DownloadSection()
            {
                Header = website.Name + " download",
                BackgroundImageUrl = "assets/img/downloads-bg.jpg",
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph(){Text =  "download Para 1"},
                    new Paragraph(){Text =  "download Para 2" }
                },
                WebsiteId = website.Id,
                isActive = true
            };
            _context.DownloadSections.Add(newdownload);
            ContactUsSection newcontactus = new ContactUsSection()
            {
                Header = website.Name + " contactus",
                Paragraphs = new List<Paragraph>()
                {
                    new Paragraph(){Text =  "contactus Para 1"}
                },
                WebsiteId = website.Id,
                isActive = true
            };
            _context.ContactUsSections.Add(newcontactus);
            List<SocialPortal> socials = new List<SocialPortal>()
            {
                new SocialPortal(){ SocialTypeId =1, url="facebook url", ContactUsId = newcontactus.Id}
            };
            newcontactus.SocialPortals = socials;

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