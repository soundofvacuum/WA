using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;
namespace MovieMark.Models
{
    public class ActorMovies:BaseEntity
    {
        public int Id { get; set; }
        public int? ActorId { get; set; }
        public Actor Actor { get; set; }
        //
        public ICollection<Movie> Movies { get; set; }
        public ActorMovies()
        {
            Movies = new List<Movie>();
        }
    }
}
