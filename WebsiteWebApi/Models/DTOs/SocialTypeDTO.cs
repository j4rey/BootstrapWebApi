﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteWebApi.Models.DTOs
{
    public class SocialTypeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CSS { get; set; }
        public int SocialPortalId { get; set; }
    }
}
