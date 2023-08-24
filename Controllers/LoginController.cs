using MvcDirectory.Models.Entities;
using MvcDirectory.Models.UserModel;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MvcDirectory.Models.Context;

namespace MvcDirectory.Controllers
{
    public class LoginController : Controller
    {
        DirectoryMvcContext db = new DirectoryMvcContext();

        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoginController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Login()
        {
            var model = new UserLoginViewModel
            {
                Kayit = new Kayit()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(Kayit p)
        {
            if (ModelState.IsValid)
            {
                Kayit kullanici = db.Kayıt.FirstOrDefault(k => k.username == @p.username && k.password == @p.password);

                if (kullanici != null)
                {

                    return RedirectToAction("Index","Person");
                }
                else
                {
                    TempData["BasarisizMesaj"] = "Geçersiz giriş bilgileri.";
                    return RedirectToAction("Login");

                }
            }
            return View(p);
        }
    }
}
