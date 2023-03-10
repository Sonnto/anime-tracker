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
