using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehber.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string CityId { get; set; }
        public string Url { get; set; }
        public string  Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        //Photo belong to city
        public City City { get; set; }

    }
}
