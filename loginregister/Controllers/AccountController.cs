using loginregister.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private static List<RegisterModel> registeredUsers = new List<RegisterModel>();

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

        // Add to mem
        registeredUsers.Add(model);

        return Ok(new { Message = "Registration successful! Please log in." });
    }

    public IActionResult LoginView()
    {
        // Display login view
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            // If model state is not valid, show errors
            return View("LoginView", model);
        }

        // Validate tcredentials
        if (IsUserAuthenticated(model.Username, model.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // if credentials are invalid
            ModelState.AddModelError("", "Invalid username or password");

            // Return the login view with the model containing the error
            return View("LoginView", model);
        }
    }

    private bool IsUserAuthenticated(string username, string password)
    {
        // Here you would typically query your database to validate the user's credentials
        // For demonstration purposes, let's check against the in-memory collection
        var user = registeredUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
        return user != null;
    }
}
