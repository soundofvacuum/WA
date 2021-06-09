using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;

namespace MovieMark.Models
{
    public class Rate:BaseEntity
    {
        public int Id { get; set; }
        public bool MovieOrActor { get; set; }
        public bool LikeOrDislike { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        //
        public int? MovieId { get; set; }
        public Movie Movie { get; set; }
        //
        public int? ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
