using MvcDirectory.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDirectory.Models.PersonModel
{
    public class PersonIndexViewModel
    {
        public List<Person> People { get; set; }

        public Person Person { get; set; }

    }
}
