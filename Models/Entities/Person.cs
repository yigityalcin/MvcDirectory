using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MvcDirectory.Models.Entities
{
    //[Table("Anything")]  bu kodu kullanıp ctrl . ile data annotations scheme kullanırsak databasedeki 
    // tablomuzun ismini kolaylıkla değiştirebiliriz
    public class Person
    {
        public int Id { get; set; }
        //[DisplayName("İsim")] bu kodu uygularsak ön tarafa gösterilen ismi istediğimiz gibi değiştirirz çünkü direkt property isimlerini alıyor front tarafı
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public IFormFile ProfilPhoto { get; set; }
        public byte[] Photo { get; set; }








    }
}
