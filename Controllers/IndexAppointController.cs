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

        private static Dictionary<int, string> Conditions = new Dictionary<int, string>();

        public IActionResult ViewAppointments()
        {
            var show = c2.AppointmentRegister.ToList();
            ViewBag.Conditions = Conditions; // Pass the conditions to the view
            return View(show);
        }

        [HttpPost]
        public IActionResult SetCondition(int id, string condition)
        {
            if (Conditions.ContainsKey(id))
            {
                Conditions[id] = condition; // Update condition
            }
            else
            {
                Conditions.Add(id, condition); // Add new condition
            }

            return RedirectToAction("ViewAppointments");
        }

        public IActionResult AddAppoint()
        {
            ViewBag.LoaderDuration = 1700; // 3 seconds

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
