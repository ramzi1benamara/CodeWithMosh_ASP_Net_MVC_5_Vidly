using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>()
            {
                new Customer(){Name = "Customer 1"},
                new Customer(){Name = "Customer 2"},
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public ActionResult Index()
        {
            // var movies = _dbContext.Movies.Include(m => m.Genre).ToList();

            // return View(movies);

            if(User.IsInRole("CanManageMovie"))
                return View("List");

            return View("ReadOnlyView");
        }

        public ActionResult Details(int id)
        {
            var movie = _dbContext.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }

        [Authorize(Roles = "CanManageMovie")]
        public ActionResult New()
        {
            var genres = _dbContext.Genres.ToList();

            var viewModel = new MovieViewModel()
            {
                Genres = genres
            };

            return View("Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            var viewModel = new MovieViewModel(movie)
            {
                Genres = _dbContext.Genres.ToList()
            };
            if (!ModelState.IsValid)
                return View("Form", viewModel);

            if (movie.Id == 0)
                _dbContext.Movies.Add(movie);
            else
            {
                var movieInDb = _dbContext.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageMovie")]
        public ActionResult Edit(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var genres = _dbContext.Genres.ToList();

            var viewModel = new MovieViewModel(movie)
            {
                Genres = genres
            };

            return View("Form", viewModel);
        }
    }
}