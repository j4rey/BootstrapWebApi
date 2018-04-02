using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models
{
    public class SocialPortal
    {
        public int Id { get; set; }

        
        public string url { get; set; }

        public int SocialTypeId { get; set; }
        public SocialType socialtype { get; set; }

        public int ContactUsId { get; set; }
        public ContactUsSection contactus { get; set; }
    }
}
