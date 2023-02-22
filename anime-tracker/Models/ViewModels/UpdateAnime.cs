using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace anime_tracker.Models.ViewModels
{
    public class UpdateAnime
    {

        //This viewmodel is a class which stores info that we need to present to /Anime/Update/{id}

        //the existing anime information

        public AnimeDto SelectedAnime { get; set; }

        //also include all animeTypes to choose from when updating this anime

        public IEnumerable<AnimeTypeDto> AnimeTypesOptions { get; set; }
    }
}