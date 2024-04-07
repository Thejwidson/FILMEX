﻿using System;
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

namespace FILMEX.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironemt;

        public SeriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironemt)
        {
            _context = context;
            _webHostEnvironemt = webHostEnvironemt;
        }

        // GET: Series
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Series.ToListAsync());
        }

        // GET: Series/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SeriesModel seriesModel)
        {
            if (ModelState.IsValid)
            {
                Models.Entities.Series series = new Models.Entities.Series();
                series.Id = seriesModel.Id;
                series.Title = seriesModel.Title;
                series.Description = seriesModel.Description;
                series.PublishDate = seriesModel.PublishDate;
                series.Rating = seriesModel.Rating;
                series.NumberOfEpisodes = seriesModel.NumberOfEpisodes;
                series.NumberOfSeasons = seriesModel.NumberOfSeasons;

                if (seriesModel.CoverImage != null)
                {
                    string folder = "series/cover/";
                    folder += Guid.NewGuid().ToString() + seriesModel.CoverImage.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironemt.WebRootPath, folder);

                    await seriesModel.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                    series.AttachmentSource = folder;
                }

                _context.Add(series);
                await _context.SaveChangesAsync();
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

            var series = await _context.Series.FindAsync(id);
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
                Rating = series.Rating,
                NumberOfEpisodes = series.NumberOfEpisodes,
                NumberOfSeasons = series.NumberOfSeasons
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
                    var seriesEntity = await _context.Series.FindAsync(id);
                    if (seriesEntity == null)
                    {
                        return NotFound();
                    }

                    seriesEntity.Title = seriesModel.Title;
                    seriesEntity.Description = seriesModel.Description;
                    seriesEntity.PublishDate = seriesModel.PublishDate;
                    seriesEntity.Rating = seriesModel.Rating;
                    seriesEntity.NumberOfEpisodes = seriesModel.NumberOfEpisodes;
                    seriesEntity.NumberOfSeasons = seriesModel.NumberOfSeasons;

                    if (seriesModel.CoverImage != null)
                    {
                        string folder = "series/cover/";
                        folder += Guid.NewGuid().ToString() + seriesModel.CoverImage.FileName;
                        string serverFolder = Path.Combine(_webHostEnvironemt.WebRootPath, folder);

                        await seriesModel.CoverImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                        seriesEntity.AttachmentSource = folder;
                    }

                    _context.Update(seriesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesExists(seriesModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
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

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.Id == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series != null)
            {
                _context.Series.Remove(series);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Episode/Create
        [Authorize(Roles = "Admin")]
        public IActionResult AddEpisode(int seriesId)
        {
            // Retrieve the selected series from the database using the provided seriesId
            var series = _context.Series.FirstOrDefault(s => s.Id == seriesId);

            // If the series is not found, handle the situation accordingly
            if (series == null)
            {
                // Handle the case where the series is not found, maybe redirect to Index or show an error message
                return RedirectToAction(nameof(Index));
            }

            // Populate ViewBag.Series with the selected series only
            ViewBag.Series = new List<SelectListItem>
            {
                new SelectListItem { Value = series.Id.ToString(), Text = series.Title, Selected = true }
            };

            // Initialize a list of SelectListItem with a default option for Season 1
            var seasons = new List<SelectListItem>
            {
                
            };
            ViewBag.Seasons = seasons;

            var episode = new FILMEX.Models.Entities.Episode();

            return View(episode);
        }

        // POST: Episode/AddEpisode
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEpisode(Episode episode)
        {
            return View();
        }

        // GET: Series/AddSeason
        [Authorize(Roles = "Admin")]
        public IActionResult AddSeason(int seriesId)
        {
            var series = _context.Series
                .Include(s => s.Seasons)
                .FirstOrDefault(s => s.Id == seriesId);

            if (series == null)
            {
                return NotFound();
            }

            // Create a new Season object to pass to the view
            var season = new Season { seriesId = seriesId }; // Assuming Season has SeriesId property

            return View(season);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSeason(int seriesId, Season season)
        {
            if (ModelState.IsValid)
            {
                var series = await _context.Series.FindAsync(seriesId);
                if (series == null)
                {
                    return NotFound();
                }

                season.seriesId = seriesId;
                _context.Add(season);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(season);
        }


        private bool SeriesExists(int id)
        {
            return _context.Series.Any(e => e.Id == id);
        }
    }
}
