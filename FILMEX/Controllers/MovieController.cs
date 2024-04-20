using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FILMEX.Data;
using FILMEX.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using FILMEX.Models;
using static NuGet.Packaging.PackagingConstants;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Globalization;
using FILMEX.Repos.Repositories;
using FILMEX.Repos.Interfaces;
using System.Xml.Linq;

namespace FILMEX.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieController _movieRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(MovieRepository movieRepository, IWebHostEnvironment webHostEnvironemt)
        {
            _movieRepository = movieRepository;
            _webHostEnvironment = webHostEnvironemt;
        }

        // GET: Movie
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();
            return View(movies);
        }

        // GET: Movie/Details/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var movie = await _movieRepository.GetMoviesWithCommentsAsync(id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // GET: Movie/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Models.Movie movie)
        {
            if (ModelState.IsValid)
            {
                Models.Entities.Movie movieEntity = new Models.Entities.Movie();
                movieEntity.Title = movie.Title;
                movieEntity.Description = movie.Description;
                movieEntity.PublishDate = movie.PublishDate;
                movieEntity.Length = movie.Length;
                movieEntity.Director = movie.Director;
                movieEntity.Screenwriter = movie.Screenwriter;
                movieEntity.Location = movie.Location;

                if (movie.CoverImage != null)
                {
                    string folder = "movies/cover/";
                    folder += Guid.NewGuid().ToString() + movie.CoverImage.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    await movie.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                    movieEntity.AttachmentSource = folder;
                }

                _movieRepository.AddMovie(movieEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }


        // GET: Movie/Edit/
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var movieEntity = await _movieRepository.FindMoviesAsync(id);

            if (movieEntity == null) return NotFound();

            var movieModel = new Models.Movie
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                Description = movieEntity.Description,
                PublishDate = movieEntity.PublishDate,
                Director = movieEntity.Director,
                Screenwriter = movieEntity.Screenwriter,
                Location = movieEntity.Location
            };

            return View(movieModel);
        }


        // POST: Movie/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Models.Movie movie)
        {
            if (id != movie.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // znajdz film o danym ID
                var movieEntity = await _movieRepository.FindMoviesAsync(id);
                if (movieEntity == null) return NotFound();
                // update danych istniejacego juz filmu
                movieEntity.Title = movie.Title;
                movieEntity.Description = movie.Description;
                movieEntity.PublishDate = movie.PublishDate;
                movieEntity.Director = movie.Director;
                movieEntity.Screenwriter = movie.Screenwriter;
                movieEntity.Location = movie.Location;

                // dodanie obrazu i zapisanie AttachmentSource
                if (movie.CoverImage != null)
                {
                    string folder = "movies/cover/";
                    folder += Guid.NewGuid().ToString() + movie.CoverImage.FileName; // nazwa pliku
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder); // sciezka do pliku

                    await movie.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create)); // upload zdjecia

                    movieEntity.AttachmentSource = folder; // przypisanie sciezki do filmu w bazie danych
                }

                // update w bazie danych
                _movieRepository.UpdateMovie(movieEntity);

                return RedirectToAction(nameof(Index)); // wroc do listy filmow
            }
            return View(movie); // zostan na stronie edycji jezeli cos jest nie tak
        }

        // GET: Movie/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var movie = _movieRepository.FindMoviesAsync(id);
            //var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRating(int MovieId, string Rating)
        {
            float rating = float.Parse(Rating, CultureInfo.InvariantCulture);
            //var movie = _context.Movies.FirstOrDefault(m => m.Id == MovieId);
            var movie = await _movieRepository.FindMoviesAsync(MovieId);

            if (movie == null) 
                return NotFound();

            // Get the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var user =await _movieRepository.FindUserAsync(userId);

            if (user == null)
                return NotFound();

            // Check if the user has already rated the movie
            var review = await _movieRepository.FindReviewByMovieIdAndUserIdAsync(MovieId, userId);

            if (review == null)
            {
                review = new ReviewMovie
                {
                    Rating = rating,
                    User = user,
                    Movie = movie
                };
                _movieRepository.AddReview(movie, review);
            }
            else
            {
                review.Rating = rating;
                _movieRepository.UpdateReview(review);
            }
            return Json(new { success = true });
        }

        // Get current user's review of the movie with the given ID to use in HTML code
        [HttpGet]
        public async Task<IActionResult> GetReview(int MovieId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await _movieRepository.FindReviewByMovieIdAndUserIdAsync(MovieId, userId);
            if (review == null)
            {
                return Json(new { rating = 0 });
            }

            return Json(new { rating = review.Rating });
        }

        // Get the average rating of the movie with the given ID to use in HTML code
        [HttpGet]
        public async Task<IActionResult> GetAverageRating(int? MovieId)
        {
            var movie = await _movieRepository.GetMoviesWithReviewsAsync(MovieId);

            if (movie == null)
            {
                return NotFound();
            }

            var averageRating = movie.Reviews.Count > 0 ? movie.Reviews.Average(r => r.Rating) : 0;

            return Json(new { averageRating });
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            //var movie = _context.Movies.Include(m => m.Comments).FirstOrDefault(m => m.Comments.Any(c => c.Id == commentId));
            var movie = await _movieRepository.FindMoviesAsync(commentId);

            if (movie == null) return NotFound();

            foreach (var comment1 in movie.Comments)
            {
                _movieRepository.LoadCommentRelations(comment1);
            }

            var comment = await _movieRepository.FindCommentByIdAsync(commentId);

            if (comment == null)
            {
                return NotFound();
            }

            _movieRepository.RemoveComment(comment);

            return View("Detail", movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieRepository.FindMoviesAsync(id);
            if (movie != null) _movieRepository.RemoveMovie(movie);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var movie = await _movieRepository.GetMoviesWithCommentsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            foreach (var comment in movie.Comments)
            {
                _movieRepository.LoadCommentRelations(comment);
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> NewComment(int id, string newComment)
        {
            var movie = await _movieRepository.GetMoviesWithCommentsAsync(id);
            if (movie == null)
                return NotFound();

            if (!string.IsNullOrEmpty(newComment))
            {
                // Get the current user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _movieRepository.FindUserAsync(userId);

                if (user == null)
                {
                       return NotFound();
                }

                var comment = new Comment
                {
                    Content = newComment,
                    Movie = movie,
                    CreatedOn = DateTime.Now,
                    Author = user
                };

               _movieRepository.AddComment(movie, comment);
            }

            return RedirectToAction("Detail", "Movie", new { id });
        }
    }
}
