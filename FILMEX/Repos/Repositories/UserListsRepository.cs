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

        public async Task<User> FindUser(string? id)
        {
            return await _context.Users.Include(u => u.MoviesToWatch).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
