using Microsoft.AspNetCore.Mvc;

namespace loginregister.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LoginView()
        {
            return View();
        }
    }
}
