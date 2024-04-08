using FILMEX.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FILMEX.Models;
using FILMEX.Models.Entities;
using FILMEX.Data;

namespace FILMEX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            Movies = _context.Movies.ToList(),
            Series = _context.Series.ToList()
        };

        return View(viewModel);
    }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Search(string searchPhrase)
        {
            var viewModel = new SearchViewModel
            {
                Actors = _context.Actors.Where(a => a.Name.Contains(searchPhrase) || a.LastName.Contains(searchPhrase)).ToList(),
                Movies = _context.Movies.Where(m => m.Title.Contains(searchPhrase)).ToList(),
                Series = _context.Series.Where(s => s.Title.Contains(searchPhrase)).ToList(),
            };

            if (!String.IsNullOrEmpty(searchPhrase))
            {
                Console.WriteLine(searchPhrase);
            }    
            else 
            {
                Console.WriteLine("PUSTE");
            }    

            return View(viewModel);
        }

        /*[Authorize(Roles = "Admin")]
        public IActionResult AdminMenu() 
        {
            return View();
        }
        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
