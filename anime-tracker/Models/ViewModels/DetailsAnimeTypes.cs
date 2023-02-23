using anime_tracker.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anime_tracker.Models.ViewModels
{
    public class DetailsAnimeTypes
    {
        public AnimeTypeDto SelectedAnimeType { get; set; }
        public IEnumerable<AnimeDto> RelatedAnimes { get; set; }
    }
}

// VIEWS AND MODEL, @ 23:35 (GO TRY AND DO UPDATE AND DELETE BEFORE COMING BACK TO THIS)