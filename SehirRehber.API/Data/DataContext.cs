using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SehirRehber.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext()
        {
            
        }
        public DbSet<Value> Values { get; set; }
    }
}
