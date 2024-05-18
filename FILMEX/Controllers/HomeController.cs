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
using FILMEX.Repos.Repositories;

namespace FILMEX.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeController _homeController;
        private readonly IMovieCategoryController _movieCatController;
        private readonly ISeriesCategoryController _seriesCatController;

        public HomeController(HomeRepository homeController, MovieCategoryRepository movieCatController, SeriesCategoryRepository seriesCatController)
        {
            _homeController = homeController;
            _movieCatController = movieCatController;
            _seriesCatController = seriesCatController;

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

            ViewBag.Categories = _movieCatController.GetAllCategories();

            return View(viewModel);

        }

        public IActionResult SeriesPage()
        {
            var viewModel = new SeriesPageViewModel
            {
                Series = _homeController.GetAllSeries()
            };

            ViewBag.Categories = _seriesCatController.GetAllCategories();

            return View(viewModel);

        }

        public IActionResult SortMoviesByCategories(string selectedCategory)
        {
            var viewModel = new SearchViewModel
            {
                Movies = _homeController.SortMoviesByCategory(selectedCategory)
            };

            if (!String.IsNullOrEmpty(selectedCategory))
            {
                Console.WriteLine(selectedCategory);
            }
            else
            {
                Console.WriteLine("PUSTE");
                return RedirectToAction("MoviesPage", "Home");
            }

            return View(viewModel);
        }

        public IActionResult SortSeriesByCategories(string selectedCategory)
        {
            var viewModel = new SearchViewModel
            {
                Series = _homeController.SortSeriesByCategory(selectedCategory)
            };

            if (!String.IsNullOrEmpty(selectedCategory))
            {
                Console.WriteLine(selectedCategory);
            }
            else
            {
                Console.WriteLine("PUSTE");
                return RedirectToAction("SeriesPage", "Home");
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
