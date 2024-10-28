using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Bank_System.Controllers
{
    public class IndexAppointController : Controller
    {
        private IndexContext c2;

        public IndexAppointController(IndexContext c2)
        {
            this.c2 = c2;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAppoint()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AddAppoint(IndexModel Appoint)
        {
            if (ModelState.IsValid)
            {
                c2.AppointmentRegister.Add(Appoint);
                c2.SaveChanges();
                return RedirectToAction("AddAppoint");
            }

            return View(Appoint);
        }

    }
}
