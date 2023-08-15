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

        public int PageSize { get; set; } // Ekledik
        public int CurrentPage { get; set; } // Ekledik
        public int TotalRecords { get; set; } // Ekledik
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize); // Ekledik

    }
}
