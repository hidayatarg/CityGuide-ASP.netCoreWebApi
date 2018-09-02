﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SehirRehber.API.Dtos
{
    public class PhotoForCreatingDto
    {
        public PhotoForCreatingDto()
        {
            DateAdded = DateTime.Now;
        }

        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }

        // Will be set automatically
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
    }
}
