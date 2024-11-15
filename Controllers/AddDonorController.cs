using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

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


        public async Task<IActionResult> ShowDonors(string searchString)
        {
            var users = await c1.AddBloodDonors.ToListAsync();
            bool dataFound = true;

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                users = users.Where(s => s.FullName.ToLower().Contains(searchString) || s.FullName.ToLower().Contains(searchString)).ToList();

                if (users.Count == 0)
                {
                    dataFound = false;
                }
            }

            ViewBag.DataFound = dataFound;
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
            return RedirectToAction("DonorsView");
        }

        [HttpGet]
        public IActionResult ExportDonorsToExcel()
        {
            var donors = c1.AddBloodDonors.ToList();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Donors");

            worksheet.Cells[1, 1].Value = "DonorId";
            worksheet.Cells[1, 2].Value = "FullName";
            worksheet.Cells[1, 3].Value = "Age";
            worksheet.Cells[1, 4].Value = "Gender";
            worksheet.Cells[1, 5].Value = "BloodType";
            worksheet.Cells[1, 6].Value = "ContactNumber";
            worksheet.Cells[1, 7].Value = "Email Address";
            worksheet.Cells[1, 8].Value = "City";
            worksheet.Cells[1, 9].Value = "State";
            worksheet.Cells[1, 10].Value = "LastDonationDate";

            for (int i = 0; i < donors.Count; i++)
            {
                var donor = donors[i];
                worksheet.Cells[i + 2, 1].Value = donor.DonorId;
                worksheet.Cells[i + 2, 2].Value = donor.FullName;
                worksheet.Cells[i + 2, 3].Value = donor.Age;
                worksheet.Cells[i + 2, 4].Value = donor.Gender;
                worksheet.Cells[i + 2, 5].Value = donor.BloodType;
                worksheet.Cells[i + 2, 6].Value = donor.ContactNumber;
                worksheet.Cells[i + 2, 7].Value = donor.Email;
                worksheet.Cells[i + 2, 8].Value = donor.City;
                worksheet.Cells[i + 2, 9].Value = donor.State;
                worksheet.Cells[i + 2, 10].Value = donor.LastDonationDate;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Donors.xlsx");
        }


    }
}
