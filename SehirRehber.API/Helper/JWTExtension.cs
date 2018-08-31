﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SehirRehber.API.Helper
{
    public static class  JwtExtension
    {
        //handle errors
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            // Jwt operation add to the header
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Allow-Origin","*");
            response.Headers.Add("Access-Control-Expose-Header","Application-Error");
        }
    }
}
