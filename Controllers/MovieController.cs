using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MyPortfolio.Controllers;

public class MovieController : Controller
{
    private readonly DataContext dbContext;

    public MovieController(DataContext dataContext)
    {
        this.dbContext = dataContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Movie> movies = new List<Movie>();
        movies = dbContext.Movies.ToList();
        return View(movies);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Store(Movie movie)
    {
        dbContext.Movies.Add(movie);
        dbContext.SaveChanges();
        return RedirectToAction("Create");
    }

    [HttpGet]
    [Route("[controller]/[action]/{id}")]
    public IActionResult Edit(int id)
    {
        var movie = dbContext.Movies.Find(id);
        return View(movie);
    }

    [HttpPost]
    [Route("[controller]/[action]/{id}")]
    public IActionResult Update(int id, Movie newMovie)
    {
        var oldMovie = dbContext.Movies.Find(id);

        oldMovie.Name = newMovie.Name;
        oldMovie.Country = newMovie.Country;
        oldMovie.Year = newMovie.Year;

        dbContext.SaveChanges();
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("[controller]/[action]/{id}")]
    public IActionResult Delete(int id)
    {
        var movie = dbContext.Movies.Find(id);
        dbContext.Movies.Remove(movie);
        dbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}