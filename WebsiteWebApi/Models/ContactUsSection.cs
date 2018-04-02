using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models
{
    public class ContactUsSection
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Header { get; set; }
        [MinLength(3)]
        public string BackgroundImageUrl { get; set; }
        public Boolean isActive { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public List<SocialPortal> SocialMedia { get; set; }
        // public LatLng Latlong { get; set; }
        
        [ForeignKey("WebsiteId")]
        public int WebsiteId { get; set; }
        public Website website { get; set; }
    }
}
