using FILMEX.Data;
using FILMEX.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FILMEX.Repos.Repositories
{
    public class MovieRepository : Interfaces.IMovieController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironemt;

        public MovieRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironemt)
        {
            _context = context;
            _webHostEnvironemt = webHostEnvironemt;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }
    }
}
