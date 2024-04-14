using Microsoft.AspNetCore.Mvc;

namespace loginregister.Models
{
    public class HomePageModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}