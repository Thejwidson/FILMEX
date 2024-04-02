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

namespace FILMEX.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironemt;

        public MovieController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironemt)
        {
            _context = context;
            _webHostEnvironemt = webHostEnvironemt;
        }

        // GET: Movie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Movie movie)
        {
            if (ModelState.IsValid)
            {
                Models.Entities.Movie movieEntity = new Models.Entities.Movie();
                movieEntity.Title = movie.Title;
                movieEntity.Description = movie.Description;
                movieEntity.PublishDate = movie.PublishDate;
                movieEntity.Rating = movie.Rating;
                movieEntity.Length = movie.Length;

                if (movie.CoverImage != null)
                {
                    string folder = "movies/cover/";
                    folder += Guid.NewGuid().ToString() + movie.CoverImage.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironemt.WebRootPath, folder);

                    await movie.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                    movieEntity.AttachmentSource = folder;
                }

                _context.Add(movieEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }


        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieEntity = await _context.Movies.FindAsync(id);
            if (movieEntity == null)
            {
                return NotFound();
            }

            var movieModel = new Models.Movie
            {
                Id = movieEntity.Id,
                Title = movieEntity.Title,
                Description = movieEntity.Description,
                PublishDate = movieEntity.PublishDate,
                Rating = movieEntity.Rating
            };

            return View(movieModel);
        }


        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // znajdz film o danym ID
                    var movieEntity = await _context.Movies.FindAsync(id);
                    if (movieEntity == null)
                    {
                        return NotFound();
                    }
                    // update danych istniejacego juz filmu
                    movieEntity.Title = movie.Title;
                    movieEntity.Description = movie.Description;
                    movieEntity.PublishDate = movie.PublishDate;
                    movieEntity.Rating = movie.Rating;

                    // dodanie obrazu i zapisanie AttachmentSource
                    if (movie.CoverImage != null)
                    {
                        string folder = "movies/cover/";
                        folder += Guid.NewGuid().ToString() + movie.CoverImage.FileName; // nazwa pliku
                        string serverFolder = Path.Combine(_webHostEnvironemt.WebRootPath, folder); // sciezka do pliku

                        await movie.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create)); // upload zdjecia

                        movieEntity.AttachmentSource = folder; // przypisanie sciezki do filmu w bazie danych
                    }

                    // update w baziedanych
                    _context.Update(movieEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) // error catch
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // wroc do listy filmow
            }
            return View(movie); // zostan na stronie edycji
        }




        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        public IActionResult Detail(int id)
        {
            var movie = _context.Movies.Include(m => m.Comments).FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            foreach (var comment in movie.Comments)
            {
                _context.Entry(comment).Reference(c => c.Author).Load();
            }

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> NewComment(int id, string newComment)
        {
            var movie = await _context.Movies.Include(m => m.Comments).FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound();

            if (!string.IsNullOrEmpty(newComment))
            {
                 // Get the current user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

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

                movie.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detail", "Movie", new { id });
        }
    }
}
