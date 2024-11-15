using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Bank_System.Controllers
{
    public class ContactusController : Controller
    {
        private ContactContext c1;

        public ContactusController(ContactContext c1)
        {
            this.c1 = c1;
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult AddContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContactUs(ContactModel contact)
        {

            if (ModelState.IsValid)
            {
                c1.Contactregister.Add(contact);
                c1.SaveChanges();
                return RedirectToAction("AddContactUs");
            }
            return View(contact);

        }
    }
}
