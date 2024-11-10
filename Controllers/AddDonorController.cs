using Microsoft.AspNetCore.Mvc;

namespace Blood_Bank_System.Controllers
{
    public class AddDonorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
