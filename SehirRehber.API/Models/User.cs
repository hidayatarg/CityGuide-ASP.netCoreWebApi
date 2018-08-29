﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehber.API.Models
{
    public class User
    {
        public User()
        {
            Cities=new List<City>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
      

        //added cities
        public List<City> Cities { get; set; }
    }
}
