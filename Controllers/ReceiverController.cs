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
            return View(take);
        }

        public IActionResult ShowReceiver()
        {
            var receivers = r2.BloodReceivers.ToList();
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


        [HttpGet]
        public IActionResult EditReceiversData(int id)
        {
            var receiver = r2.BloodReceivers.Find(id);
            if (receiver == null)
            {
                return NotFound();
            }

            return View(receiver);
        }

        // POST: EditReceiversData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReceiversData(ReceiveBloodModel receiver)
        {
            if (ModelState.IsValid)
            {
                r2.BloodReceivers.Update(receiver);
                r2.SaveChanges();

                return RedirectToAction("ReceiverView");
            }

            return View(receiver);
        }

        public IActionResult ReceiverDelete(int Id)
        {
            var data = r2.BloodReceivers.Find(Id);
            if (data != null)
            {
                r2.BloodReceivers.Remove(data);
                r2.SaveChanges();

                TempData["SuccessMessage1"] = "Receiver deleted successfully!";

            }
            return RedirectToAction("ReceiverView");
        }

    }
}
