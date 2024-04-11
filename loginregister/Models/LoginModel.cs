using Microsoft.AspNetCore.Mvc;

namespace loginregister.Models
{
    public class LoginModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
