using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteWebApi.Models;

namespace WebsiteWebApi.Repositories
{
    public class WebSiteContext : DbContext
    {
        //readonly Dictionary<string, object> defaults;
        public WebSiteContext(DbContextOptions<WebSiteContext> options) : base(options) {
            //defaults = new Dictionary<string, object>()
            //{
            //    { nameof(Website.IsDeleted), false },
            //    { nameof(HomeSection.isActive), true },
            //    { nameof(AboutSection.isActive), true },
            //    { nameof(DownloadSection.isActive), true },
            //    { nameof(ContactUsSection.isActive), true }

            //};
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeSection>().Property(p => p.isActive).HasDefaultValue(true);

            //foreach (var matchingProp in modelBuilder.Entity<HomeSection>()
            //.Metadata
            //.GetProperties()
            //.Where(x => defaults.ContainsKey(x.Name)))
            //{
            //    matchingProp.Relational().DefaultValue = defaults[matchingProp.Name];
            //}

            modelBuilder.Entity<Website>().Property(p => p.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<AboutSection>().Property(p => p.isActive).HasDefaultValue(true);
            modelBuilder.Entity<DownloadSection>().Property(p => p.isActive).HasDefaultValue(true);
            modelBuilder.Entity<ContactUsSection>().Property(p => p.isActive).HasDefaultValue(true);
            modelBuilder.Entity<SocialType>().HasMany(x => x.SocialPortals);
        }

        public DbSet<Website> Websites { get; set; }
        public DbSet<HomeSection> HomeSections { get; set; }
        public DbSet<AboutSection> AboutSections { get; set; }
        public DbSet<DownloadSection> DownloadSections { get; set; }
        public DbSet<ContactUsSection> ContactUsSections { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<SocialPortal> SocialPortals { get; set; }
        public DbSet<SocialType> SocialTypes { get; set; }
    }
}
