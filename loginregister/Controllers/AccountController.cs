using loginregister.Models;
using Microsoft.AspNetCore.Mvc;

namespace loginregister.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Register
        public IActionResult RegisterView()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            // Process registration logic here...
            // Assuming registration logic is completed successfully

            // Redirect to LoginView after successful registration
            return RedirectToAction("LoginView");
        }

        // GET: /Account/Login
        public IActionResult LoginView()
        {
            // Implement your login action/view here
            return View();
        }
    }
}
