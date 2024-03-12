using FILMEX.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FILMEX.Data
{
    public class ApplicationDbContext : DbContext
    {
        

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FILMEX;Trusted_Connection=True");
            base.OnConfiguring(optionsBuilder);
        }


    }
}
