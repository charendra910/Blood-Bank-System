using Blood_Bank_System.Data;
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

    }
}
