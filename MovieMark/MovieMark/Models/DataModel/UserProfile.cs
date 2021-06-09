using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;

namespace MovieMark.Models
{
    public class UserProfile:BaseEntity
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PathtoAvatar { get; set; }

        public User User { get; set; }
    }
}
