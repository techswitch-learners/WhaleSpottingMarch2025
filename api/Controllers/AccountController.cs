using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WhaleSpottingBackend.Models.RequestModels;
using WhaleSpottingBackend.Models.DatabaseModels;

[ApiController]
[Route("api/[controller]")]
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
            await _userManager.AddToRoleAsync(user,"User");  
            return Ok(new { message = "Registration successful." });
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if(user == null) {
            return BadRequest("The username does not exist in our system.");
        }
        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {           
            return Ok(new { message = "Login successful." });
        }
        return BadRequest("Incorrect username and password.");
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
}