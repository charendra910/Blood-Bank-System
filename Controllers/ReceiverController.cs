using Blood_Bank_System.Data;
using Blood_Bank_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Bank_System.Controllers
{
    public class ReceiverController : Controller
    {
        private ReceiveBloodContext r2;

        public ReceiverController(ReceiveBloodContext r2)
        {
            this.r2 = r2;
        }

        [HttpGet]
        public IActionResult Receive()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Receive(ReceiveBloodModel take)
        {
            if (ModelState.IsValid)
            {
                r2.BloodReceivers.Add(take);
                r2.SaveChanges();
                TempData["SuccessMessage"] = "Receiver data added successfully.";

                return RedirectToAction("Receive");
            }
            // If ModelState is not valid, return the view with validation errors
            return View(take);
        }

    }
}
