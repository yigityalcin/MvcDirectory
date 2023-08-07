using Microsoft.AspNetCore.Http;
using MvcDirectory.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDirectory.Models.PersonModel
{
    public class PersonDetailViewModel
    {
        public Person Person { get; set; }
        public List<Person> People { get; set; }
        public IFormFile UpdatePhoto { get; set; }

    }
}
