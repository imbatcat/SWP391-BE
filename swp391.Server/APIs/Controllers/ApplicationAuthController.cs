using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.Auth;
using PetHealthcare.Server.Models.ApplicationModels;
using PetHealthcare.Server.Services.Interfaces;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApplicationAuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAccountService _context;

    public ApplicationAuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountService context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AccountDTO registerAccount)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = registerAccount.UserName,
                Email = registerAccount.Email,
                AccountFullname = registerAccount.FullName,
            };
            var result = await _userManager.CreateAsync(user, registerAccount.Password);
            if (result.Succeeded)
            {
                await _context.CreateAccount(registerAccount);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { message = "User registered successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginModel registerAccount)
    {
        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(registerAccount.UserName, registerAccount.Password, registerAccount.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
        }

        // If we got this far, something failed, redisplay form
        return BadRequest("Check username and password");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return Ok("User logged off");
    }
    // Other custom endpoints (login, logout, etc.)
}
