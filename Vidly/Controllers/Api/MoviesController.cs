using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Authorize(Roles = RoleName.CanManageMovie)]
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //GET /api/movies
        [AllowAnonymous]
        public IHttpActionResult GetMovies()
        {
            return Ok(_dbContext.Movies.Include(m=>m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>));
        }


        [AllowAnonymous]
        //GET /api/movies/[id]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri($"{Request.RequestUri}/{movieDto.Id}"), movieDto);
        }

        [HttpPut]
        //PUT /api/customers/[id]
        public IHttpActionResult UpdateMovie(MovieDto movieDto, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            _dbContext.SaveChanges();

            return Ok(movieDto);
        }

        [HttpDelete]
        //DELETE /api/movies/[id]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            _dbContext.Movies.Remove(movie);

            _dbContext.SaveChanges();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
    }
}