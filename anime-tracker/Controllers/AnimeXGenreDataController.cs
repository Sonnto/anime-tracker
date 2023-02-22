using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using anime_tracker.Models;

namespace anime_tracker.Controllers
{
    public class AnimeXGenreDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // =============== READ(LIST) ===============
        // GET: api/AnimeXGenreData/ListAnimeXGenres
        [HttpGet]
        public IEnumerable<AnimeXGenreDto> ListAnimeXGenres()
        {
            List<AnimeXGenre> animeXGenres = db.AnimeXGenres.ToList();
            List<AnimeXGenreDto> animeXGenreDtos = new List<AnimeXGenreDto>();

            animeXGenres.ForEach(a => animeXGenreDtos.Add(new AnimeXGenreDto()
            {
                animexgenre_id = a.animexgenre_id,
                anime_id = a.anime_id,
                genre_name = a.Genres.genre_name
            }));

            return animeXGenreDtos;
        }
        // =============== READ(FIND) ===============
        // GET: api/AnimeXGenreData/FindAnimeXGenre/5
        [ResponseType(typeof(AnimeXGenre))]
        [HttpGet]
        public IHttpActionResult FindAnimeXGenre(int id)
        {
            AnimeXGenre animeXGenre = db.AnimeXGenres.Find(id);
            AnimeXGenreDto animeXGenreDto = new AnimeXGenreDto()
            {
                animexgenre_id = animeXGenre.animexgenre_id,
                anime_id = animeXGenre.anime_id,
                genre_name = animeXGenre.Genres.genre_name
            };

            if (animeXGenre == null)
            {
                return NotFound();
            }

            return Ok(animeXGenreDto);
        }
        // =============== UPDATE ===============
        // PUT: api/AnimeXGenreData/UpdateAnimeXGenre/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAnimeXGenre(int id, AnimeXGenre animeXGenre)
        {
            Debug.WriteLine("I have reached the update AnimeXGenre method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != animeXGenre.animexgenre_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST mismatch" + animeXGenre.animexgenre_id);
                return BadRequest();
            }

            db.Entry(animeXGenre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeXGenreExists(id))
                {
                    Debug.WriteLine("AnimeXGenre associate not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // =============== CREATE(ADD) ===============
        // POST: api/AnimeXGenreData/AddAnimeXGenre
        [ResponseType(typeof(AnimeXGenre))]
        [HttpPost]
        public IHttpActionResult AddAnimeXGenre(AnimeXGenre animeXGenre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AnimeXGenres.Add(animeXGenre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = animeXGenre.animexgenre_id }, animeXGenre);
        }

        // =============== DELETE ===============
        // DELETE: api/AnimeXGenreData/DeleteAnimeXGenre/5
        [ResponseType(typeof(AnimeXGenre))]
        [HttpPost]
        public IHttpActionResult DeleteAnimeXGenre(int id)
        {
            AnimeXGenre animeXGenre = db.AnimeXGenres.Find(id);
            if (animeXGenre == null)
            {
                return NotFound();
            }

            db.AnimeXGenres.Remove(animeXGenre);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimeXGenreExists(int id)
        {
            return db.AnimeXGenres.Count(e => e.animexgenre_id == id) > 0;
        }
    }
}