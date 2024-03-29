﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.CloudDesignPatterns
{
    public class PhotoViewModel
    {
        public string Description { get; set; }

        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}
