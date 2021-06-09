using MovieMark.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMark.Models
{
    public class Actor:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public int Score { get; set; }
        public string PathtoPhoto { get; set; }
        //
        public int? MovieId { get; set; }
        public Movie Movie { get; set; }
        //
        public ICollection<ActorMovies> ActorMovies { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public Actor()
        {
            ActorMovies = new List<ActorMovies>();
            Rates = new List<Rate>();
        }
    }
}
