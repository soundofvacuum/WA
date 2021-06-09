using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;
namespace MovieMark.Models
{
    public class User:BaseEntity
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public UserProfile Profile { get; set; }
        //
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public User()
        {
            Comments = new List<Comment>();
            Rates = new List<Rate>();
        }
    }
}
