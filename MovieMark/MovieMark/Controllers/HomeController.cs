using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieMark.Models;
using MovieMark.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MovieMark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MovieMarkContext db;

        
        public HomeController(ILogger<HomeController> logger)
        {
            this.db = new MovieMarkContext();
            _logger = logger;
        }

       // [HttpPost]
        public IActionResult MovieView(string field)
        {
            if (field == "Title")
                return View(db.Movies.ToList().OrderBy(p => p.Title));
            else
           if (field == "ReleaseDate")
                return View(db.Movies.ToList().OrderBy(p => p.ReleaseDate));
            else
           if (field == "Score")
                return View(db.Movies.ToList().OrderByDescending(p => p.Score));
            else
                return View(db.Movies.ToList().OrderBy(p => p.Title));
        }
        //[HttpPost]
        public IActionResult ActorView(string field)
        {
            if (field == "Title")
                return View(db.Actors.ToList().OrderBy(p => p.Name));
            else
           if (field == "Score")
                return View(db.Actors.ToList().OrderByDescending(p => p.Score));
            else
                return View(db.Actors.ToList().OrderBy(p => p.Name));
        }
        //Лайки/дизлайки
        [Authorize]
        public ViewResult MakeRate(int Id,bool MovieOrActor, bool LikeOrDislike)
        {
            Rate rate = new Rate();
            User user = new User();
            user = db.Users
                .Where(u => u.Login == User.Identity.Name)
                .First();
            if(MovieOrActor==true)
            {
                rate.MovieOrActor = true;
                rate.MovieId = Id;
                rate.LikeOrDislike = LikeOrDislike;
                rate.User = user;
            }
            if (MovieOrActor == false)
            {
                rate.MovieOrActor = false;
                rate.ActorId = Id;
                rate.LikeOrDislike = LikeOrDislike;
                rate.User = user;
            }
            db.Rates.Add(rate);
            db.SaveChanges();
            if (MovieOrActor == true)
            {
                Movie movie = new Movie();
                movie = db.Movies
                    .Where(m => m.Id == Id)
                    .First();
                List<Rate> rates = new List<Rate>();
                foreach (var x in db.Rates)
                {
                    if(x.MovieId==Id)
                    rates.Add(x);
                }
                double score = 0,buff=0;
                for (int i=0;i<rates.Count();i++)
                {
                    if (rates[i].LikeOrDislike == true) buff +=10;
                    if (rates[i].LikeOrDislike == false) buff += 0;
                }
                score =( 5 + buff )/ (rates.Count()+1);
                movie.Score = score;
                db.Movies.Update(movie);
                db.SaveChanges();
            }
            if (MovieOrActor == false)
            {
                Actor actor = new Actor();
                actor = db.Actors
                    .Where(m => m.Id == Id)
                    .First();
                List<Rate> rates = new List<Rate>();
                foreach (var x in db.Rates)
                {
                    if (x.ActorId == Id)
                        rates.Add(x);
                }
                double score = 0, buff = 0;
                for (int i = 0; i < rates.Count(); i++)
                {
                    if (rates[i].LikeOrDislike == true) buff +=10;
                    if (rates[i].LikeOrDislike == false) buff +=0;
                }
                score =( 5 + buff )/ (rates.Count()+1);
                actor.Score = (int)score;
                db.Actors.Update(actor);
                db.SaveChanges();
            }
            return View("Index");
            }
        //Комментарии к фильмам
        [Authorize]
        public IActionResult CommentView(int Id)
        {
            ViewBag.Id = Id;
            var model = new MoviesandActors()
            {
                Movies = db.Movies.ToList().Where(m=>m.Id==Id),
                Comments = db.Comments.ToList().Where(m => m.MovieId == Id),
                Rates = db.Rates.ToList().Where(m => m.MovieId == Id)
            };
            return View(model);
        }
        public ViewResult MakeComment(int Id, string CommentText)
        {
            Movie movie = new Movie();
            movie = db.Movies
                .Where(u => u.Id == Id)
                .First();
            User user = new User();
            user = db.Users
                .Where(u => u.Login == User.Identity.Name)
                .First();
            Comment comment = new Comment();
            comment.Text = CommentText;
            comment.WrittenDate = DateTime.Now;
            comment.User = user;
            comment.Movie = movie;
            db.Comments.Add(comment);
            db.SaveChanges();
            return View("Index");
        }
        //
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult ProfileView()
        {
            User user = new User();
            user = db.Users
                .Where(u => u.Login == User.Identity.Name)
                .First();
            UserProfile userProfile = new UserProfile();
                userProfile = user.Profile;
            return View(user);
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
