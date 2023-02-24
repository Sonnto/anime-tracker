using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anime_tracker.Models.ViewModels
{
    public class DetailsAnime
    {
        public AnimeDto SelectedAnime { get; set; }
        public IEnumerable<GenreDto> TaggedGenres { get; set; }
        public IEnumerable <GenreDto> AvailableGenres { get; set; }
    }
}