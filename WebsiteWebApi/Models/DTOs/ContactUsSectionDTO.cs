using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models.DTOs
{
    public class ContactUsSectionDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string BackgroundImageUrl { get; set; }
        public Boolean isActive { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public int WebsiteId { get; set; }
    }
}
