using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;

namespace MovieMark.Models
{
    public class Comment:BaseEntity
    {
        public int Id { get; set; }
        public DateTime WrittenDate { get; set; }
        public string Text { get; set; }
        //
        public int? UserId { get; set; }
        public User User { get; set; }
        //
        public int? MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
