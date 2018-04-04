using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models.DTOs
{
    public class WebsiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }

        //public string HomeHeader { get; set; }
        //public string HomeBackgroundImageUrl { get; set; }
        //public List<String> HomeParagraphs { get; set; }

        //public string AboutHeader { get; set; }
        //public string AboutBackgroundImageUrl { get; set; }
        //public List<String> AboutParagraphs { get; set; }

        //public string DownloadHeader { get; set; }
        //public string DownloadBackgroundImageUrl { get; set; }
        //public List<String> DownloadParagraphs { get; set; }

        //public string ContactUsHeader { get; set; }
        //public List<String> ContactUsParagraphs { get; set; }
        //public string ContactUsBackgroundImageUrl { get; set; }
        //public List<SocialTypeDTO> Socials { get; set; }
    }

    public class WebsitesDTO
    {
        public WebsiteDTO website { get; set; }
        public HomeSectionDTO Home { get; set; }
        public AboutSectionDTO about { get; set; }
        public DownloadSectionDTO download { get; set; }
        public ContactUsSectionDTO contactus { get; set; }
    }
}
