using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SehirRehber.API.Models;

namespace SehirRehber.API.Dtos
{
    //Place the inforamation you want to send to the user
    public class CityForDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }

        //a city has list of photos
        public List<Photo> Photos { get; set; }
    }
}
