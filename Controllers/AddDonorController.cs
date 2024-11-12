using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Bank_System.Controllers
{
    public class AddDonorController : Controller
    {
        private AddDonorContext c1;

        public AddDonorController(AddDonorContext c1)
        {
            this.c1 = c1;
        }

        public IActionResult AddDonors()
        {
            return View();
        }

        // POST action to handle form submission
        [HttpPost]
        public IActionResult AddDonors(AddDonorModel adddonor)
        {
            if (ModelState.IsValid)
            {
                c1.AddBloodDonors.Add(adddonor);
                c1.SaveChanges();

                TempData["SuccessMessage"] = "Donor added successfully!";
                return RedirectToAction("AddDonors"); // Redirect to a different action, e.g., Index
            }

            return View(adddonor); // Return the same view if validation fails
        }

        public IActionResult ShowDonors()
        {
            var show = c1.AddBloodDonors.ToList();
            return View(show);
        }

        public IActionResult DonorsView()
        {
            var show = c1.AddBloodDonors.ToList();
            return View(show);
        }
        public IActionResult EditDonors()
        {
            return View();
        }

    }
}
