using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models
{
    public class Website
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        //[MinLength(3)]
        public string Name { get; set; }
        [Required]
        //[MinLength(5)]
        public string WebsiteUrl { get; set; }

        public Boolean IsDeleted { get; set; }
        
        public HomeSection home { get; set; }
        public AboutSection about { get; set; }
        public DownloadSection download { get; set; }
        public ContactUsSection contactus { get; set; }
    }
}
