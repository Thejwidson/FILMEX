using FILMEX.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FILMEX.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieCategory> MoviesCategories { get; set; }
        public DbSet<User> Users {  get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FILMEX;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }*/
        //Nie ruszajcie na razie, nie pytajcie czemu bo sam nie wiem ~Dawidzior

    }
}
