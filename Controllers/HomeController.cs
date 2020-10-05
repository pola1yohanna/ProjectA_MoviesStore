using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ProjectA_Movies.Data;
using ProjectA_Movies.Models;
namespace ProjectA_Movies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult ViewMovies()
        {
            IList<Movie> movies = _context.Movies.ToList();
            return View(movies);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Movie movie)
        {
            if (!ModelState.IsValid)
                return NotFound();
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return View("ViewMovies");
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
        [HttpPost]
        public IActionResult Search(string name, string actionpage)
        {
            
            return actionpage == "" ? RedirectToAction("Index") : RedirectToAction(actionpage, new { name });
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(e=>e.Id==id);
            if (movie==null)
            {
                return BadRequest();
            }    
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string name)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(e => e.Name == name);
            if (movie==null)
            {
                return NotFound();
            }
            return View(movie);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(e => e.Name == name);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(Movie EditedMovie)
        {
            if(EditedMovie==null)
            {
                return BadRequest();
            }
            _context.Movies.Update(EditedMovie);
            await _context.SaveChangesAsync();
            return View("Index");
        }
    }
}
