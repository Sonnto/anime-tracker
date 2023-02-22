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
    public class AnimeTypeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // =============== READ(LIST) ===============
        // GET: api/AnimeTypeData/ListAnimeTypes
        [HttpGet]
        public IEnumerable<AnimeTypeDto> ListAnimeTypes()
        {
            List<AnimeType> AnimeTypes = db.AnimeTypes.ToList();
            List<AnimeTypeDto> AnimeTypeDtos = new List<AnimeTypeDto>();

            AnimeTypes.ForEach(at => AnimeTypeDtos.Add(new AnimeTypeDto()
            {
                anime_type_id = at.anime_type_id,
                anime_type_name = at.anime_type_name,
            })) ;
            return AnimeTypeDtos;
        }
        // =============== READ(FIND) ===============
        // GET: api/AnimeTypeData/FindAnimeType/5
        [ResponseType(typeof(AnimeType))]
        [HttpGet]
        public IHttpActionResult FindAnimeType(int id)
        {
            AnimeType animeType = db.AnimeTypes.Find(id);
            AnimeTypeDto AnimeTypeDto = new AnimeTypeDto()
            {
                anime_type_id = animeType.anime_type_id,
                anime_type_name = animeType.anime_type_name,
            };
            if (animeType == null)
            {
                return NotFound();
            }

            return Ok(AnimeTypeDto);
        }
        // Because the Anime Types are static (there will not be new anime mediums of deliver), AnimeTypeDataController will not need to create, update, or delete any of them.
    }
}