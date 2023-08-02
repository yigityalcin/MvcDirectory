using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MvcDirectory.Models.Context;
using MvcDirectory.Models.Entities;
using MvcDirectory.Models.PersonModel;
using MvcDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesseract;
using System.Diagnostics;
using System.IO;

namespace MvcDirectory.Controllers
{
    public class PersonController : Controller
    {
        DirectoryMvcContext db = new DirectoryMvcContext();

        public IActionResult Index()
        {
            var model = new PersonIndexViewModel
            {
                People = db.People.OrderBy(p => p.Name).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PersonAddViewModel
            {
                Person = new Person()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            try
            {
                // Eğer fotoğraf yüklendi ise, fotoğrafın byte dizisini alıp Kisi.Foto alanına atayın
                if (person.ProfilPhoto != null && person.ProfilPhoto.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        person.ProfilPhoto.CopyTo(memoryStream);
                        person.Photo = memoryStream.ToArray();
                    }
                }

                db.People.Add(person);
                db.SaveChanges();
                TempData["BasariliMesaj"] = "Kayıt Başarılı";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["BasarisizMesaj"] = "Kayıt işlemi başarısız. Lütfen yeniden deneyin.";
                return RedirectToAction("Add");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var person = db.People.Find(id);

            if (person == null)
            {
                TempData["HataliMesaj"] = "No Records Found!";
                return RedirectToAction("Index");
            }

            var model = new PersonUpdateViewModel
            {
                Person = person
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Person person)
        {
            var formerPerson = db.People.Find(person.Id);
            if (formerPerson == null)
            {
                TempData["HataliMesaj"] = "No Record Found";
                return RedirectToAction("Index");
            }

            formerPerson.Name = person.Name;
            formerPerson.PhoneNumber = person.PhoneNumber;
            formerPerson.Email = person.Email;

            db.SaveChanges();

            TempData["BasariliMesaj"] = "Person information has been successfully updated.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var person = db.People.Find(id);
            if (person == null)
            {
                TempData["HataliMesaj"] = "Person not found";
                return RedirectToAction("Index");
            }

            var model = new PersonDetailViewModel
            {
                Person = person
            };
            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = db.People.Find(id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            db.People.Remove(person);
            db.SaveChanges();

            TempData["BasariliMesaj"] = "Person has been successfully deleted.";

            return RedirectToAction("Index");
        }

        public IActionResult Search(string searchName)
        {
            var people = db.People.ToList();

            if (!string.IsNullOrEmpty(searchName))
            {
                people = people
                    .Where(k => k.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                people = people
                    .OrderBy(p => p.Name)
                    .ToList();
            }

            var model = new PersonIndexViewModel
            {
                People = people
            };

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Read()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Read(Person person)
        {
            var result = GetTextFromImage("a4.png");

            result.PhoneNumber = result.PhoneNumber.Replace(" ", "");
            result.PhoneNumber = result.PhoneNumber.Replace("-", "");

            person.Name = result.Name;
            person.PhoneNumber = result.PhoneNumber;
            person.Email = result.Email;

            return View("Read", person);
        }

        private Person GetTextFromImage(string imagePath)
        {
            using (var engine = new TesseractEngine("./tessdata", "tur", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine(text);

                        string phoneNumber = string.Join(',', Extractor.ExtractPhoneNumber(text));
                        string email = string.Join(',', Extractor.ExtractEmailAddresses(text));
                        string name = Extractor.ExtractName(text);

                        Console.WriteLine("Name: " + name);
                        Console.WriteLine("Phone Number: " + phoneNumber);
                        Console.WriteLine("Email Address: " + email);

                        var person = new Person
                        {
                            Name = name,
                            PhoneNumber = phoneNumber,
                            Email = email,
                        };

                        return person;
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult ReadSave(Person person)
        {
            try
            {
                db.People.Add(person);
                db.SaveChanges();
                TempData["BasariliMesaj"] = "Kayıt Başarılı";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["BasarisizMesaj"] = "Kayıt işlemi başarısız. Lütfen yeniden deneyin.";
                return RedirectToAction("Read");
            }
        }
  

        [HttpPost]
        public IActionResult AddPersonalNotes(PersonDetailViewModel model)
        {
            var person = db.People.Find(model.Person.Id);
            if (person == null)
            {
                TempData["HataliMesaj"] = "Person not found";
                return RedirectToAction("Index");
            }

            // Update the personal notes for the person
            person.PersonalNotes = model.Person.PersonalNotes;

            db.SaveChanges();

            TempData["BasariliMesaj"] = "Personal notes added successfully.";

            // Redirect back to the detail view
            return RedirectToAction("Detail", new { id = model.Person.Id });
        }


    }
}
