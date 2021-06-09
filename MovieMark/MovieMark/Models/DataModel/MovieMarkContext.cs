using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieMark.Models.DataModel
{
    public class MovieMarkContext:DbContext
    {
        public MovieMarkContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovies> ActorMovies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-FEGNGN1\\SQLEXPRESS;Database=MovieMarkDb;Trusted_Connection=True;");
        }
        
    }
}
