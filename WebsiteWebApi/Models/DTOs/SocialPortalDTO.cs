using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models.DTOs
{
    public class SocialPortalDTO
    {
        public int Id { get; set; }


        public string url { get; set; }

        public SocialTypeDTO socialtype { get; set; }

        public int ContactUsId { get; set; }
    }
    
}
