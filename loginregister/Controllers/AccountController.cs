using loginregister.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

public class AccountController : Controller
{
    private static List<RegisterModel> registeredUsers = new List<RegisterModel>();

    public IActionResult RegisterView()
    {
        return View(new RegisterModel());
    }

    public IActionResult LoginView()
    {
        return View(new LoginModel());
    }

    public IActionResult DashboardView()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegisterView(RegisterModel model)
    {
        // Manually remove PasswordHash and PasswordSalt from model state
        ModelState.Remove(nameof(model.PasswordHash));
        ModelState.Remove(nameof(model.PasswordSalt));

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            // Return validation errors as JSON response
            return BadRequest(new { Errors = errors });
        }

        Console.WriteLine($"Received RegisterModel: Email={model.Email}, Username={model.Username}, PasswordHash={(model.PasswordHash != null ? "Populated" : "Not populated")}, PasswordSalt={(model.PasswordSalt != null ? "Populated" : "Not populated")}");

        // Hash the password using PBKDF2
        (model.PasswordHash, model.PasswordSalt) = HashPassword(model.Password);

        // Check if PasswordHash and PasswordSalt are populated
        if (model.PasswordHash == null || model.PasswordSalt == null)
        {
            ModelState.AddModelError("", "PasswordHash or PasswordSalt was not generated correctly.");
            return BadRequest(new { Errors = "PasswordHash or PasswordSalt was not generated correctly." });
        }

        // Create a new user with necessary fields
        var newUser = new RegisterModel
        {
            Email = model.Email,
            Username = model.Username,
            AgreeTerms = model.AgreeTerms,
            PasswordHash = model.PasswordHash,
            PasswordSalt = model.PasswordSalt
        };

        // Add the user to the list of registered users
        registeredUsers.Add(newUser);

        return Ok(new { Message = "Registration successful! Please log in." });
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            // If model state is not valid, show errors
            return View("LoginView", model);
        }

        // Check if the user exists
        var user = registeredUsers.FirstOrDefault(u => u.Username == model.Username);

        if (user == null)
        {
            // User not found, display "Invalid username or password"
            ModelState.AddModelError("", "Invalid username or password");
            return View("LoginView", model);
        }




        // Retrieve or initialize login attempt count for the user
        int loginAttemptCount = HttpContext.Session.GetInt32("LoginAttemptCount") ?? 0;

        // Check if the user has exceeded the allowed login attempts
        if (loginAttemptCount == 2)
        {
            ModelState.AddModelError("", "Login attempts exceeded. Please register again.");
            return View("LoginView", model);
        }

        // Validate credentials
        if (IsUserAuthenticated(model.Username, model.Password))
        {
            // Reset login attempt count upon successful login
            HttpContext.Session.SetInt32("LoginAttemptCount", 0);
            return RedirectToAction("DashboardView", "Account"); // Redirect to home page upon successful login
        }
        else
        {
            // Calculate remaining attempts
            int remainingAttempts = 2 - loginAttemptCount;

            // Increment login attempt count and save it in session
            loginAttemptCount++;
            HttpContext.Session.SetInt32("LoginAttemptCount", loginAttemptCount);


            ModelState.AddModelError("", $"Invalid username or password. {remainingAttempts} attempts remaining.");
            return View("LoginView", model); // Return to login view with error message
        }
    }

    private bool IsUserAuthenticated(string username, string password)
    {
        var user = registeredUsers.FirstOrDefault(u => u.Username == username);

        if (user == null)
            return false;

        // Validate the password using PBKDF2
        return ValidatePassword(password, user.PasswordHash, user.PasswordSalt);
    }

    private bool ValidatePassword(string password, byte[] savedHash, byte[] savedSalt)
    {
        using (var deriveBytes = new Rfc2898DeriveBytes(password, savedSalt, 10000))
        {
            byte[] inputHash = deriveBytes.GetBytes(20); // 20 is the size of the hash

            // Compare the hashes
            for (int i = 0; i < 20; i++)
            {
                if (inputHash[i] != savedHash[i])
                    return false;
            }
        }

        return true;
    }

    // Helper method to hash the password using PBKDF2
    private (byte[], byte[]) HashPassword(string password)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000))
        {
            byte[] hash = deriveBytes.GetBytes(20); // 20 is the size of the hash

            return (hash, salt);
        }
    }
}
