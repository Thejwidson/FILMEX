using FILMEX.Data;
using FILMEX.Models.Entities;
using FILMEX.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FILMEX.Repos.Repositories
{
    public class UserListsRepository : IUserListsController
    {
        private readonly ApplicationDbContext _context;
        public UserListsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Movie> GetAllMovies()
        {
            return _context.Movies.ToList();
        }

        public List<Series> GetAllSeries()
        {
            return _context.Series.ToList();
        }

        public async Task<User> FindUserWithMovies(string? id)
        {
            return await _context.Users.Include(u => u.MoviesToWatch).FirstOrDefaultAsync(u => u.Id == id);
        }

        public User FindUserWithMoviesNotAsync(string? id)
        {
            return  _context.Users.Include(u => u.MoviesToWatch).FirstOrDefault(u => u.Id == id);
        }

        public async Task<User> FindUserWithSeries(string? id)
        {
            return await _context.Users.Include(u => u.SeriesToWatch).FirstOrDefaultAsync(u => u.Id == id);
        }

        public User FindUserWithSeriesNotAsync(string? id)
        {
            return _context.Users.Include(u => u.SeriesToWatch).FirstOrDefault(u => u.Id == id);
        }
    }
}
