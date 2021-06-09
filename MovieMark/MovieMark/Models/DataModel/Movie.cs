using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;

namespace MovieMark.Models
{
    public class Movie:BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Score { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public string PathToImg { get; set; }
        //
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Movie()
        {
            Actors = new List<Actor>();
            Rates = new List<Rate>();
            Comments = new List<Comment>();
        }
    }
}
