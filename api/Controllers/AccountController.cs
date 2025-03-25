using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WhaleSpottingBackend.Models.RequestModels;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{   
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;
    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        var user = new IdentityUser
        {
            UserName = model.Name,
            Email = model.Email,
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {           
            return Ok(new { message = "Registration successful" });
        }
        return BadRequest(result.Errors);
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