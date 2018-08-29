using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SehirRehber.API.Models;

namespace SehirRehber.API.Data
{
    public class DataContext:DbContext
    {
        //Injection settings
        public DataContext(DbContextOptions<DataContext> options): base (options)
        {
            
        }
        public DbSet<Value> Values { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
