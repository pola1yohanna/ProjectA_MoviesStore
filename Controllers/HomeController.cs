using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectA_Movies.Models;
namespace ProjectA_Movies.Controllers
{
    public class HomeController : Controller
    {
        public List<Movie> _Movies = new List<Movie>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ViewMovies(List<Movie> movies)
        {
            _Movies = movies;
            return View(_Movies);
        }
        public IActionResult Index()
        {
            _Movies = new List<Movie>();
            Movie movie = new Movie()
            {
                Name = "The Prestige",
                Genre = "Drama"
            };
            _Movies.Add(movie);
            return View(_Movies);
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
}
