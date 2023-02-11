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
    public class AnimeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // =============== READ(LIST) ===============
        // GET: api/AnimeData/ListAnimes
        [HttpGet]
        public IEnumerable<AnimeDto> ListAnimes()
        {
            List<Anime> Animes = db.Animes.ToList();
            List<AnimeDto> AnimeDtos = new List<AnimeDto>();

            Animes.ForEach(a => AnimeDtos.Add(new AnimeDto()
            {
                anime_id = a.anime_id,
                anime_title = a.anime_title,
                anime_type_name = a.AnimeTypes.anime_type_name,
                rating = a.rating,
                activity = a.activity,
                favorite = a.favorite,
            }));
            return AnimeDtos;
        }
        // =============== READ(FIND) ===============
        // GET: api/AnimeData/FindAnime/5
        [ResponseType(typeof(Anime))]
        [HttpGet]
        public IHttpActionResult FindAnime(int id)
        {
            Anime anime = db.Animes.Find(id);
            AnimeDto AnimeDto = new AnimeDto()
            {
                anime_id = anime.anime_id,
                anime_title = anime.anime_title,
                anime_type_name = anime.AnimeTypes.anime_type_name,
                rating = anime.rating,
                activity = anime.activity,
                favorite = anime.favorite,
            };
            if (anime == null)
            {
                return NotFound();
            }

            return Ok(AnimeDto);
        }
        // =============== UPDATE ===============
        // POST: api/AnimeData/UpdateAnime/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAnime(int id, Anime anime)
        {
            Debug.WriteLine("Reached Update Anime method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != anime.anime_id)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + anime.anime_id);
                return BadRequest();
            }

            db.Entry(anime).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimeExists(id))
                {
                    Debug.WriteLine("Anime not found");
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
        // POST: api/AnimeData/AddAnime
        [ResponseType(typeof(Anime))]
        [HttpPost]
        public IHttpActionResult AddAnime(Anime anime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animes.Add(anime);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = anime.anime_id }, anime);
        }
        // =============== DELETE ===============
        // POST: api/AnimeData/DeleteAnime/5
        [ResponseType(typeof(Anime))]
        [HttpPost]
        public IHttpActionResult DeleteAnime(int id)
        {
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return NotFound();
            }

            db.Animes.Remove(anime);
            db.SaveChanges();

            return Ok(anime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimeExists(int id)
        {
            return db.Animes.Count(e => e.anime_id == id) > 0;
        }
    }
}