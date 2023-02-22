using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using anime_tracker.Migrations;
using anime_tracker.Models;
using System.Diagnostics;

namespace anime_tracker.Controllers
{
    public class GenreDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // =============== READ(LIST) ===============
        // GET: api/GenreData/ListGenre
        [HttpGet]
        public IEnumerable<GenreDto> ListGenres()
        {
            List<Genre> Genres = db.Genres.ToList();
            List<GenreDto> GenreDtos = new List<GenreDto>();

            Genres.ForEach(g => GenreDtos.Add(new GenreDto()
            {
                genre_id = g.genre_id,
                genre_name = g.genre_name,
            }));
            return GenreDtos;
        }
        // =============== READ(FIND) ===============
        // GET: api/AnimeData/FindAnime/5
        [ResponseType(typeof(Genre))]
        [HttpGet]
        public IHttpActionResult FindGenre(int id)
        {
            Genre genre = db.Genres.Find(id);
            GenreDto GenreDto = new GenreDto()
            {
                genre_id = genre.genre_id,
                genre_name = genre.genre_name,
            };
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(GenreDto);
        }
        // Because Genres are static (there will not be new genres), GenreDataController will not need to create, update, or delete any of them.
    }
}