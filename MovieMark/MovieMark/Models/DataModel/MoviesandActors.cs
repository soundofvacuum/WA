using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMark.Models.DataModel
{
    public class MoviesandActors
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Rate> Rates { get; set; }
    }
}
