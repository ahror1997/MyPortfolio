using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.Controllers;

public class UserController : Controller
{
    private readonly DataContext dbContext;

    public UserController(DataContext dataContext)
    {
        this.dbContext = dataContext;
    }

    [Authorize]
    public IActionResult Index()
    {
        List<User> odamlar = new List<User>();
        odamlar = dbContext.Users.ToList();
        return View(odamlar);
    }

    [HttpPost]
    public IActionResult Store(User odam)
    {
        odam.Link = "test1";
        odam.Name = "testName";
        odam.PhoneNumber = "42432423";
        dbContext.Users.Add(odam);
        dbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}