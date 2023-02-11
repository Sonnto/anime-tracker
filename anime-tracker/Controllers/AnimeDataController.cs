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
using anime_tracker.Models;

namespace anime_tracker.Controllers
{
    public class AnimeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // =============== READ(LIST) ===============
        // GET: api/AnimeData/ListAnimes
        public IQueryable<Anime> ListAnimes()
        {
            return db.Animes;
        }
        // =============== READ(FIND) ===============
        // GET: api/AnimeData/FindAnime/5
        [ResponseType(typeof(Anime))]
        public IHttpActionResult FindAnime(int id)
        {
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return NotFound();
            }

            return Ok(anime);
        }
        // =============== UPDATE ===============
        // POST: api/AnimeData/UpdateAnime/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAnime(int id, Anime anime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anime.anime_id)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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