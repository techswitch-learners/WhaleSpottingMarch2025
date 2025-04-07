using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WhaleSpottingBackend.Models.DatabaseModels;
using WhaleSpottingBackend.Models.RequestModels;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        var user = new User
        {
            Name = model.Name,
            UserName = model.UserName,
            Email = model.Email,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            var createdUser = await _userManager.FindByNameAsync(user.UserName);
            if (createdUser == null)
            {
                return BadRequest("Some error occurred while creating user. Created user not found.");
            }
            await _userManager.AddToRoleAsync(createdUser, "User");

            // Login the user
            var response = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (response.Succeeded)
            {
                // Adding a cookie
                CreateCookie("UserRole", "User");
                return Ok(new { message = "Registration successful and user logged in." });
            }
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            return Unauthorized("The username does not exist in our system.");
        }
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
        var role = await _userManager.GetRolesAsync(user);

        if (result.Succeeded)
        {
            // Adding a cookie
            CreateCookie("UserRole", role[0]);
            return Ok(new { message = "Login successful." });
        }
        return Unauthorized("Incorrect username and password.");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        HttpContext.Response.Cookies.Delete("UserRole");
        return Ok(new { message = "Logged out successfully." });
    }

    [HttpGet("Public")]
    public IActionResult All()
    {
        return Ok("Accessible to all.");
    }

    [HttpGet("User")]
    [Authorize]
    public IActionResult UserEndPoint()
    {
        return Ok("Accessible to users only");
    }

    [HttpGet("Admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminEndpoint()
    {
        return Ok("Accessible to admin only.");
    }

    private void CreateCookie(string name, string value)
    {
        HttpContext.Response.Cookies.Append(name, value, new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddHours(1),
            HttpOnly = false,
            IsEssential = true
        });
    }
}
