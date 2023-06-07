using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.Controllers;

public class PersonController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Test()
    {
        return View();
    }
}