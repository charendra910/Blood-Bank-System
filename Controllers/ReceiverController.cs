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

        public IActionResult ShowReceiver()
        {
            var receivers = r2.BloodReceivers.ToList(); // Assuming _context is your database context
            return View(receivers);
        }


        public IActionResult EditReceiversData()
        {
            return View();
        }

        public IActionResult ReceiverView()
        {
            var rcb = r2.BloodReceivers.ToList();
            return View(rcb);

        }

        // GET: EditReceiversData
        // GET: EditReceiversData
        [HttpGet]
        public IActionResult EditReceiversData(int id)
        {
            var receiver = r2.BloodReceivers.Find(id); // Fetch receiver by id
            if (receiver == null)
            {
                return NotFound(); // Handle case where receiver is not found
            }

            return View(receiver); // Pass the receiver to the view
        }

        // POST: EditReceiversData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReceiversData(ReceiveBloodModel receiver)
        {
            if (ModelState.IsValid)
            {
                r2.BloodReceivers.Update(receiver); // Update the receiver in the database
                r2.SaveChanges(); // Save changes to the database

                return RedirectToAction("ReceiverView"); // Redirect to another page after update
            }

            return View(receiver); // Return the same view with validation errors if any
        }


        //public IActionResult EditReceiversData(int ReceiverID)
        //{
        //    var editdata = r2.BloodReceivers.Find(ReceiverID);

        //    if (editdata == null)
        //    {
        //        return NotFound();

        //    }
        //    return View(editdata);

        //}

        //[HttpPost]
        //public IActionResult EditReceiversData(ReceiveBloodModel editdata)
        //{
        //    r2.BloodReceivers.Update(editdata);
        //    r2.SaveChanges();

        //    return RedirectToAction("ReceiversView");

        //}

        //public IActionResult Editcertificate(int id)
        //{
        //    var data = c1.Certificatedetails.Find(id);
        //    if (data == null)
        //    {
        //        return NotFound(); // Or handle the case where the student with the given id is not found
        //    }
        //    return View(data);
        //}

        //[HttpPost]
        //public IActionResult Editcertificate(CertificateModel data)
        //{
        //    c1.Certificatedetails.Update(data);
        //    c1.SaveChanges();

        //    return RedirectToAction("Certificateview");
        //}


    }
}
