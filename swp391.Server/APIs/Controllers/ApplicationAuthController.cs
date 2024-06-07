using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.Auth;
using PetHealthcare.Server.Models.ApplicationModels;
using PetHealthcare.Server.Services;
using PetHealthcare.Server.Services.Interfaces;
using System.Text.Encodings.Web;
using System.Text;
using PetHealthcare.Server.Services.AuthInterfaces;
using NuGet.Common;
using PetHealthcare.Server.Helpers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApplicationAuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IEmailSender _emailService;
    private readonly IAccountService _accountService;
    private readonly IAuthenticationService _authenticationService;

    public ApplicationAuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IEmailSender emailService, IAccountService context, IAuthenticationService authenticationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _accountService = context;
        _authenticationService = authenticationService;
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

            try
            {
                var account = await _accountService.CreateAccount(registerAccount);
                var role = Helpers.GetRole(registerAccount.RoleId);
                var result = await _userManager.CreateAsync(user, registerAccount.Password);
                if (result.Succeeded)
                {
                    await _authenticationService.SendConfirmationEmail(user.Id, user.Email);
                    await _userManager.AddToRoleAsync(user, role);
                    return Ok(new { message = "User registered successfully" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex);
            }
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseUserDTO>> Login([FromBody] LoginModel loginAccount)
    {
        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var user = await _userManager.FindByNameAsync(loginAccount.UserName);
            if (user == null)
            {
                return BadRequest("No such username");
            }
            if (!user.EmailConfirmed)
            {
                return BadRequest("Account is not confirmed");
            }
            var result = await _signInManager.PasswordSignInAsync(loginAccount.UserName, loginAccount.Password, loginAccount.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(new ResponseUserDTO
                {
                    id = (await _accountService.GetAccountByCondition(x => x.Username == user.UserName)).AccountId,
                    role = await _authenticationService.GetUserRole(user)
                });
            }
        }

        // If we got this far, something failed, redisplay form
        return BadRequest("Incorrect password");
    }

    [AllowAnonymous]
    [HttpPost("get-role")]
    public async Task<string?> GetRole(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return await _authenticationService.GetUserRole(user);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return Ok("User logged off");
    }

    [AllowAnonymous]
    [HttpGet("confirmemail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(token);
        string code = Encoding.UTF8.GetString(base64EncodedBytes);
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            await _accountService.SetAccountIsDisabled(new RequestAccountDisable
            {
                username = user.UserName,
                IsDisabled = false
            });
            await _signInManager.SignInAsync(user, isPersistent: false);
        }
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("sendconfirmemail")]
    public async Task<IActionResult> SendConfirmEmail([FromBody] ConfirmReqUser user)
    {
        await _authenticationService.SendConfirmationEmail(user.UserId, user.Email);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> SendForgotPasswordEmail([FromBody] PasswordReqUser user)
    {
        try
        {

            var _user = await _userManager.FindByEmailAsync(user.Email);
            if (!(await _userManager.IsEmailConfirmedAsync(_user)))
            {
                return BadRequest("Account is not activated");
            }
            await _authenticationService.SendForgotPasswordEmail(_user, user.Email);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] RequestResetPassword entity)
    {
        var user = await _userManager.FindByIdAsync(entity.UserId);
        IdentityResult result;
        try
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(entity.Token);
            string code = Encoding.UTF8.GetString(base64EncodedBytes);
            result = await _userManager.ResetPasswordAsync(user, code, entity.NewPassword);
        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
        }

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return Ok();
    }
    [AllowAnonymous]
    [HttpGet("setrole")]
    public async Task<IActionResult> SetRole([FromQuery] string userName, [FromQuery] string role)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, role);

        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

        return Ok();
    }
}
