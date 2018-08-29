using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehber.API.Models
{
    public class City
    {
        public City()
        {
            Photos= new List<Photo>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //city photos (city has many photos)
        public List<Photo> Photos { get; set; }

        //city added by user
        public User User { get; set; }
    }
}
