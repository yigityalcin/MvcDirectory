using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDirectory.Models.Entities
{
    public class Kayit
    {
        public int Id { get; set; }
        //[DisplayName("İsim")] bu kodu uygularsak ön tarafa gösterilen ismi istediğimiz gibi değiştirirz çünkü direkt property isimlerini alıyor front tarafı
        public string username { get; set; }
        public string password { get; set; }
        public string Email { get; set; }


    }
}
