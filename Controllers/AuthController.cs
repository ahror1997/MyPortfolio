using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyPortfolio.Controllers;

public class AuthController : Controller
{
    private readonly DataContext dbContext;

    public AuthController(DataContext dataContext)
    {
        this.dbContext = dataContext;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendRegister(User odam)
    {
        odam.Link = "test1";
        odam.Name = "testName";
        odam.PhoneNumber = "42432423";
        dbContext.Users.Add(odam);
        dbContext.SaveChanges();

        List<Claim> claims = new List<Claim>()
        { 
            new Claim(ClaimTypes.NameIdentifier, odam.Email),
            new Claim("OtherProperties","Example Role")
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        AuthenticationProperties properties = new AuthenticationProperties()
        { 
            AllowRefresh = true,
            IsPersistent = true
        };

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

        return RedirectToAction("Index", "User");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendLogin(User odam, string returnUrl)
    {
        returnUrl ??= "/Home/Index";
        var user = dbContext.Users.FirstOrDefault(t => t.Email == odam.Email && t.Password == odam.Password);

        List<Claim> claims = new List<Claim>()
        { 
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("First Name", user.FirstName),
            new Claim("OtherProperties","Example Role")
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        AuthenticationProperties properties = new AuthenticationProperties()
        { 
            AllowRefresh = true,
            IsPersistent = true
        };

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

        return LocalRedirect(returnUrl);
    }


    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}