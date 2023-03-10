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
        [ResponseType(typeof(AnimeDto))]
        public IHttpActionResult ListAnimes()
        {
            List<Anime> Animes = db.Animes.ToList();
            List<AnimeDto> AnimeDtos = new List<AnimeDto>();

            Animes.ForEach(a => AnimeDtos.Add(new AnimeDto()
            {
                anime_id = a.anime_id,
                anime_title = a.anime_title,
                anime_type_id = a.anime_type_id,
                anime_type_name = a.AnimeTypes.anime_type_name,
                status = a.status,
                start_date = a.start_date,
                end_date = a.end_date,
                activity = a.activity,
                completed_episodes = a.completed_episodes,
                total_episodes = a.total_episodes,
                rating = a.rating,
                favorite = a.favorite,
            })) ;
            return Ok(AnimeDtos);
        }

        // =============== READ(LIST) ===============
        // GET: api/AnimeData/ListAnimesForAnimeType
        [HttpGet]
        [ResponseType(typeof(AnimeDto))]
        public IHttpActionResult ListAnimesForAnimeType(int id)
        {
            List<Anime> Animes = db.Animes.Where(a=>a.anime_type_id==id).ToList();
            List<AnimeDto> AnimeDtos = new List<AnimeDto>();

            Animes.ForEach(a => AnimeDtos.Add(new AnimeDto()
            {
                anime_id = a.anime_id,
                anime_title = a.anime_title,
                anime_type_id = a.anime_type_id,
                anime_type_name = a.AnimeTypes.anime_type_name,
                status = a.status,
                start_date = a.start_date,
                end_date = a.end_date,
                activity = a.activity,
                completed_episodes = a.completed_episodes,
                total_episodes = a.total_episodes,
                rating = a.rating,
                favorite = a.favorite,
            }));

            Debug.WriteLine("AnimeDataController.cs: the anime_type_id is: " + AnimeDtos);

            return Ok(AnimeDtos);
        }

        // =============== READ(LIST) ===============
        // GET: api/AnimeData/ListAnimesForGenre
        [HttpGet]
        [ResponseType(typeof(AnimeDto))]
        public IHttpActionResult ListAnimesForGenre(int id)
        {
            //ask anime that have genres that match our ID
            List<Anime> Animes = db.Animes.Where(
                a=>a.Genres.Any(g=>g.genre_id==id)).ToList();
            List<AnimeDto> AnimeDtos = new List<AnimeDto>();

            Animes.ForEach(a => AnimeDtos.Add(new AnimeDto()
            {
                anime_id = a.anime_id,
                anime_title = a.anime_title,
                anime_type_id = a.anime_type_id,
                anime_type_name = a.AnimeTypes.anime_type_name,
                status = a.status,
                start_date = a.start_date,
                end_date = a.end_date,
                activity = a.activity,
                completed_episodes = a.completed_episodes,
                total_episodes = a.total_episodes,
                rating = a.rating,
                favorite = a.favorite,
            }));
            return Ok(AnimeDtos);
        }

        // POST: api/AnimeData/AssociateAnimeWithGenre/{anime_id}/{genre_id}
        [HttpPost]
        [Route("api/animedata/associateanimewithgenre/{anime_id}/{genre_id}")]
        public IHttpActionResult AssociateAnimeWithGenre(int anime_id, int genre_id)
        {
            Anime selectedAnime = db.Animes.Include
                (a => a.Genres).Where
                (a => a.anime_id == anime_id).FirstOrDefault();
            Genre selectedGenre = db.Genres.Find(genre_id);

            if(selectedAnime == null || selectedGenre == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Input anime id is: " + anime_id);
            Debug.WriteLine("Selected anime title is: " + selectedAnime.anime_title);
            Debug.WriteLine("Input genre id to be added: " + genre_id);
            Debug.WriteLine("Selected genre name to be added: " + selectedGenre.genre_name);

            selectedAnime.Genres.Add(selectedGenre);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/AnimeData/UnAssociateAnimeWithGenre/{anime_id}/{genre_id}
        [HttpPost]
        [Route("api/animedata/unassociateanimewithgenre/{anime_id}/{genre_id}")]
        public IHttpActionResult UnAssociateAnimeWithGenre(int anime_id, int genre_id)
        {
            Anime selectedAnime = db.Animes.Include(a => a.Genres).Where(a => a.anime_id == anime_id).FirstOrDefault();
            Genre selectedGenre = db.Genres.Find(genre_id);

            if (selectedAnime == null || selectedGenre == null)
            {
                return NotFound();
            }
            Debug.WriteLine("Input anime id is: " + anime_id);
            Debug.WriteLine("Selected anime title is: " + selectedAnime.anime_title);
            Debug.WriteLine("Input genre id to be removed: " + genre_id);
            Debug.WriteLine("Selected genre name to be removed: " + selectedGenre.genre_name);

            selectedAnime.Genres.Remove(selectedGenre);
            db.SaveChanges();

            return Ok();
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
                anime_type_id = anime.anime_type_id,
                anime_type_name = anime.AnimeTypes.anime_type_name,
                status = anime.status,
                start_date = anime.start_date,
                end_date = anime.end_date,
                activity = anime.activity,
                completed_episodes = anime.completed_episodes,
                total_episodes = anime.total_episodes,
                rating = anime.rating,
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
                Debug.WriteLine("GET parameter" + " " + id);
                Debug.WriteLine("POST parameter" + " " + anime.anime_id);
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

        private bool AnimeExists(int id)
        {
            return db.Animes.Count(e => e.anime_id == id) > 0;
        }
    }
}