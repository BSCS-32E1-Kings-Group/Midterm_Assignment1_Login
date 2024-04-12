using Microsoft.AspNetCore.Mvc;

namespace loginregister.Models
{
    public class RegisterModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
