using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMark.Models.DataModel;
using MovieMark.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MovieMark.Controllers
{
    public class FormsController : Controller
    {
        private MovieMarkContext db;

        public FormsController()
        {
            this.db = new MovieMarkContext();
        }
        [Authorize]
        public IActionResult AddForm()
        {
            var model = new MoviesandActors()
            {
                Movies = db.Movies.ToList(),
                Actors = db.Actors.ToList()
            };
            return View(model);
        }
      
        //Добавление фильма
        [HttpPost]
        public void AddMovie(string Title, DateTime ReleaseDate, string Director, string Description, string PathToImg)
        {
            Movie movies = new Movie();
            movies.Title = Title;
            movies.ReleaseDate = ReleaseDate;
            movies.Director = Director;
            movies.Description = Description;
            movies.Score = 5;
            movies.PathToImg = PathToImg;
            db.Movies.Add(movies);
            db.SaveChanges();
        }
        //Добавление Актёра
        [HttpPost]
        public void AddActor(string Name, DateTime BornDate, string PathToPhoto)
        {
            Actor actors = new Actor();
            actors.Name = Name;
            actors.BornDate = BornDate;
            actors.Score = 5;
            actors.PathtoPhoto = PathToPhoto;
            db.Actors.Add(actors);
            db.SaveChanges();
        }
        //Добавление Актёрского состава  - не реализовано (((
        [HttpPost]
        public void AddStuff(int[] selectedActors, int MovieID)
        {
          /*  
            Movie movie = new Movie();

            movie = db.Movies
                .Where(b => b.Id == MovieID)
                .First();
            List<Movie> movies = new List<Movie>();
            movies.Add(movie);

            foreach (var x in selectedActors)
            {
                ActorMovies stuff = new ActorMovies();
                Actor actor = new Actor();
                actor = db.Actors
                    .Where(a => a.Id == x)
                    .First();
                stuff.ActorId = x;
                stuff.Movies = movies;
                stuff.Actor = actor;
                db.ActorMovies.Add(stuff);
            }
            db.SaveChanges();
            */
        }
        //Авторизация/Регистрация/Аутентификация
        public IActionResult RegistrationForm()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult AutorizationForm()
        {
            return View();
        }
        public IActionResult Register(string Login, string Password, string Name, string Email, string PathToAvatar)
        {
            
            User users = new User();
            UserProfile profiles = new UserProfile();
            users.Login = Login;
            users.Password = Password;
            users.Role = 0;
            db.Users.Add(users);
            db.SaveChanges();
            User user = new User();
            user = db.Users.Where(u => u.Login == Login).First();
            profiles.Name = Name;
            profiles.Email = Email;
            profiles.PathtoAvatar = PathToAvatar;
            
            profiles.User = user;
            user.Profile = profiles; 
            
            db.UserProfiles.Add(profiles);
            db.Users.Update(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AutorizationForm(User model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("AutorizationForm", "Forms");
        }
    }
}
    
