using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;

namespace MyPortfolio.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataContext dataContext;

    public HomeController(ILogger<HomeController> logger, DataContext dataContext)
    {
        _logger = logger;
        this.dataContext = dataContext;
    }

    public IActionResult Index()
    {
        string user_agent = HttpContext.Request.Headers.UserAgent.ToString();
        string ip = HttpContext.Connection.RemoteIpAddress.ToString();
        ViewData["user_agent"] = user_agent;
        ViewData["ip"] = ip;
        return View();
    }

    private void AddNewUser()
    {
        User odamcha = new User()
        {
            Name = "Messi",
            PhoneNumber = "46516541651",
            Password = "123456",
            Link = "barca",
            FirstName = "Leo",
            LastName = "Messi",
            Email = "Messi@mail.ru",
            City = "Paris"
        };

        dataContext.Users.Add(odamcha);
        dataContext.SaveChanges();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
