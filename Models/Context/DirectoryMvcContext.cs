using MvcDirectory.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDirectory.Models.Context
{
    public class DirectoryMvcContext:DbContext
    {
        public DirectoryMvcContext():base("Server=.;Database=DirectoryMvcDB;Trusted_Connection=true")
        {

        } 

        public DbSet<Person> People { get; set; }
        public DbSet<Kayit> Kayıt { get; set; }




    }
}
