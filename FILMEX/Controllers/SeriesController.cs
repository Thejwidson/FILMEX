using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FILMEX.Data;
using FILMEX.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using FILMEX.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Globalization;
using FILMEX.Repos.Interfaces;
using FILMEX.Repos;
using FILMEX.Repos.Repositories;

namespace FILMEX.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesController _seriesRepository;
        private readonly ISeriesCategoryController _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironemt;

        public SeriesController(SeriesRepository seriesRepository, SeriesCategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironemt)
        {
            _seriesRepository = seriesRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironemt = webHostEnvironemt;
        }

        // GET: Series
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var series = await _seriesRepository.GetAllAsync();
            return View(series);
        }

        // GET: Series/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _seriesRepository.FindById(id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // GET: Series/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAllCategories();
            return View();
        }

        // POST: Series/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SeriesModel seriesModel, List<int> SelectedCategories)
        {
            if (ModelState.IsValid)
            {
                Series series = new Series();
                series.Id = seriesModel.Id;
                series.Title = seriesModel.Title;
                series.Description = seriesModel.Description;
                series.PublishDate = seriesModel.PublishDate;
                series.Director = seriesModel.Director;
                series.Screenwriter = seriesModel.Screenwriter;
                series.Location = seriesModel.Location;

                if (seriesModel.CoverImage != null)
                {
                    string folder = "series/cover/";
                    folder += Guid.NewGuid().ToString() + seriesModel.CoverImage.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironemt.WebRootPath, folder);

                    await seriesModel.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                    series.AttachmentSource = folder;
                }

                // Assign categories from SelectedCategories to the movieEntity
                foreach (var categoryIterator in SelectedCategories)
                {
                    var category = _categoryRepository.GetCategoryById(categoryIterator);
                    if (category != null)
                    {
                        series.Categories.Add(category);
                        _categoryRepository.AddSeriesToCategory(series, categoryIterator);
                    }
                }

                await _seriesRepository.Add(series);
                return RedirectToAction(nameof(Index));
            }
            return View(seriesModel);
        }

        // GET: Series/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _seriesRepository.FindById(id);
            
            if (series == null)
            {
                return NotFound();
            }

            var seriesModel = new SeriesModel
            {
                Id = series.Id,
                Title = series.Title,
                Description = series.Description,
                PublishDate = series.PublishDate,
                Director = series.Director,
                Screenwriter = series.Screenwriter,
                Location = series.Location
            };
            return View(seriesModel);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, SeriesModel seriesModel)
        {
            if (id != seriesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var seriesEntity = await _seriesRepository.FindById(id);
                    if (seriesEntity == null)
                    {
                        return NotFound();
                    }

                    seriesEntity.Title = seriesModel.Title;
                    seriesEntity.Description = seriesModel.Description;
                    seriesEntity.PublishDate = seriesModel.PublishDate;
                    seriesEntity.Director = seriesModel.Director;
                    seriesEntity.Screenwriter = seriesModel.Screenwriter;
                    seriesEntity.Location = seriesModel.Location;


                    if (seriesModel.CoverImage != null)
                    {
                        string folder = "series/cover/";
                        folder += Guid.NewGuid().ToString() + seriesModel.CoverImage.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironemt.WebRootPath, folder);

                        await seriesModel.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                        seriesEntity.AttachmentSource = folder;
                    }
                    await _seriesRepository.Update(seriesEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_seriesRepository.Any(seriesModel.Id)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seriesModel);
        }


        // GET: Series/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _seriesRepository.FindById(id);

            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRating(int SeriesId, string Rating)
        {
            float rating = float.Parse(Rating, CultureInfo.InvariantCulture);
            var series = await _seriesRepository.FindById(SeriesId);

            if (series == null) {
                return NotFound();
            }

            // Get the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _seriesRepository.FindUserAsync(userId);

            if (user == null) {
                return NotFound();
            }

            // Check if the user has already rated the movie
            var review = _seriesRepository.FindReview(SeriesId, userId);
            if (review == null)
            {
                review = new ReviewSeries
                {
                    Rating = rating,
                    User = user,
                    Series = series
                };
                _seriesRepository.AddReview(series, review);
            }
            else
            {
                review.Rating = rating;
                _seriesRepository.UpdateReview(review);
            }

            return Json(new { success = true });
        }

        // Get current user's review of the movie with the given ID to use in HTML code
        [HttpGet]
        public async Task<IActionResult> GetReview(int SeriesId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = _seriesRepository.FindReviewByIdAndUId(SeriesId, userId);

            if (review == null){
                return Json(new { rating = 0 });
            }

            return Json(new { rating = review.Rating });
        }

        // Get the average rating of the movie with the given ID to use in HTML code
        [HttpGet]
        public IActionResult GetAverageRating(int SeriesId)
        {
            var series = _seriesRepository.FindByIdIncludeReviews(SeriesId);

            if (series == null)
            {
                return NotFound();
            }

            var averageRating = series.Reviews.Count > 0 ? series.Reviews.Average(r => r.Rating) : 0;

            return Json(new { averageRating });
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var series = await _seriesRepository.FindById(id);
            if (series != null){
                await _seriesRepository.Remove(series);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Episode/Create
        [Authorize(Roles = "Admin")]
        public IActionResult AddEpisode(int seriesId, int seasonId)
        {
            var series = _seriesRepository.FindByIdWithSeasons(seriesId);

            if (series == null){
                return RedirectToAction(nameof(Index));
            }

            var seasons = new List<SelectListItem>();
            foreach (Season season in series.Seasons){
                seasons.Add(new SelectListItem { Value = season.Id.ToString(), Text = season.Title, Selected = (season.Id == seasonId) });
            }

            ViewBag.Seasons = seasons;

            var episode = new Episode { SeasonId = seasonId, SeriesId = seriesId };

            return View(episode);
        }

        // POST: Episode/AddEpisode
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEpisode(int seasonId, int seriesId, Episode episode)
        {
            if (ModelState.IsValid)
            {
                episode.SeasonId = seasonId;
                episode.SeriesId = seriesId;
                await _seriesRepository.Add(episode);

                return RedirectToAction(nameof(Index));
            } 
            return View(episode);
        }

        // POST: Series/AddSeason
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSeason(int seriesId, Season season)
        {
            var series = _seriesRepository.FindByIdWithSeasons(seriesId);
            string title;

            if (series == null || !series.Seasons.Any())
            {
                season.SeasonNumber = 1;
                title = "Season 1";
            }
            else
            {
                var lastSeasonNumber = series.Seasons.Max(s => s.SeasonNumber);
                season.SeasonNumber = lastSeasonNumber + 1;
                title = $"Season {lastSeasonNumber + 1}";
            }

            season.SeriesId = series.Id;
            season.Title = title;
            await _seriesRepository.Add(season);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            var serie = _seriesRepository.FindWithComments(id);
            if (serie == null)
            {
                return NotFound();
            }

            foreach (var comment in serie.Comments)
            {
                _seriesRepository.LoadCommentRelations(comment);
            }

            _seriesRepository.LoadCategoryRelations(serie);

            return View(serie);
        }

        [HttpPost]
        public async Task<IActionResult> NewComment(int id, string newComment)
        {
            var series = _seriesRepository.FindWithComments(id);
            if (series == null)
                return NotFound();

            if (!string.IsNullOrEmpty(newComment))
            {
                // Get the current user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _seriesRepository.FindUserAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                var comment = new Comment
                {
                    Content = newComment,
                    Production = series,
                    CreatedOn = DateTime.Now,
                    Author = user
                };
                await _seriesRepository.Add(series, comment);
            }

            return RedirectToAction("Detail", "Series", new { id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var series = await _seriesRepository.FindByCommentIdAsync(commentId);

            if (series == null) return NotFound();

            foreach (var comment1 in series.Comments)
            {
                _seriesRepository.LoadCommentRelations(comment1);
            }

            var comment = await _seriesRepository.FindCommentByIdAsync(commentId);

            if (comment == null){
                return NotFound();
            }

            await _seriesRepository.Remove(series, comment);

            return View("Detail", series);
        }
        [HttpPost]
        public async Task<IActionResult> AddToWatchList(int SerieId)
        {
            var serie = await _seriesRepository.FindSeriesAsync(SerieId);

            if (serie == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await _context.Users.FindAsync(userId);
            var user = await _seriesRepository.FindUserAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var serieToWatch = user.SeriesToWatch.FirstOrDefault(s => s.Id == serie.Id);

            if (serieToWatch == null)
            {
                await _seriesRepository.AddSerieToWatch(serie, user);
            }

            //return View();
            return RedirectToAction("Detail", "Series", new { id = SerieId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchList(int SerieId)
        {
            var serie = await _seriesRepository.FindSeriesAsync(SerieId);

            if (serie == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _seriesRepository.FindUserAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            await _seriesRepository.RemoveSerieToWatch(serie, user);

            return RedirectToAction("ToWatch", "UserLists");
        }
    }
}
