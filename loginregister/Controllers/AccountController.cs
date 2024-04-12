using loginregister.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult RegisterView()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegisterView(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            // Collect all validation error messages
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            // Return validation errors as JSON response
            return BadRequest(new { Errors = errors });
        }

        // Simulate registration (for demonstration purposes only)
        // Here you would typically handle registration logic (e.g., saving to a database)
        TempData["Message"] = "Registration successful! Please log in.";

        // Return a success message as JSON response
        return Ok(new { Message = "Registration successful" });
    }

    public IActionResult LoginView()
    {
        // Implement your login action/view here
        return View();
    }
}