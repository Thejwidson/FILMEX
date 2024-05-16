using FILMEX.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FILMEX.Models;
using FILMEX.Models.Entities;
using FILMEX.Data;
using FILMEX.Repos.Interfaces;
using FILMEX.Repos;

namespace FILMEX.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeController _homeController;

        public HomeController(HomeRepository homeController)
        {
            _homeController = homeController;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Movies = _homeController.GetAllMovies(),
                Series = _homeController.GetAllSeries()
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
                Actors = _homeController.SearchActors(searchPhrase),
                Movies = _homeController.SearchMovies(searchPhrase),
                Series = _homeController.SearchSeries(searchPhrase)
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

        public IActionResult MoviesPage() 
        {
            var viewModel = new MoviesPageViewModel
            {
                Movies = _homeController.GetAllMovies()
            };

            return View(viewModel);

        }

        public IActionResult SeriesPage()
        {
            var viewModel = new SeriesPageViewModel
            {
                Series = _homeController.GetAllSeries()
            };

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
