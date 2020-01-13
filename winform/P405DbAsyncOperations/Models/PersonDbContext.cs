using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P405DbAsyncOperations.Models
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext() : base("myDb") { }

        public DbSet<Person> People { get; set; }
       
    }
    
}
