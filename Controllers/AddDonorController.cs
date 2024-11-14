using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public IActionResult AddDonors(AddDonorModel adddonor)
        {
            if (ModelState.IsValid)
            {
                c1.AddBloodDonors.Add(adddonor);
                c1.SaveChanges();

                TempData["SuccessMessage"] = "Donor added successfully!";
                return RedirectToAction("AddDonors");
            }

            return View(adddonor);
        }

        //public IActionResult ShowDonors()
        //{
        //    var show = c1.AddBloodDonors.ToList();
        //    return View(show);
        //}
        public async Task<IActionResult> ShowDonors(string searchString)
        {
            var users = await c1.AddBloodDonors.ToListAsync();
            bool dataFound = true; // Flag to indicate if data was found

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower(); // Convert search string to lowercase

                users = users.Where(s => s.FullName.ToLower().Contains(searchString) || s.FullName.ToLower().Contains(searchString)).ToList();

                if (users.Count == 0)
                {
                    dataFound = false; // Set flag to false if no data is found
                }
            }

            ViewBag.DataFound = dataFound; // Pass the flag to the view
            return View(users);

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

        [HttpGet]
        public IActionResult EditDonors(int id)
        {
            var donator = c1.AddBloodDonors.Find(id);
            if (donator == null)
            {
                return NotFound();
            }

            return View(donator);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReceiversData(AddDonorModel donator)
        {
            if (ModelState.IsValid)
            {
                c1.AddBloodDonors.Update(donator);
                c1.SaveChanges();

                return RedirectToAction("DonorsView");
            }

            return View(donator);
        }

        public IActionResult DonorDelete(int Id)
        {
            var data = c1.AddBloodDonors.Find(Id);
            if (data != null)
            {
                c1.AddBloodDonors.Remove(data);
                c1.SaveChanges();

                TempData["SuccessMessage1"] = "Donor deleted successfully!";

            }
            return RedirectToAction("DonorsView"); // Replace "Index" with the appropriate action name or URL
        }



    }
}
