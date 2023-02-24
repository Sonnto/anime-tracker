using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anime_tracker.Models.ViewModels
{
    public class DetailsGenre
    {
        public GenreDto SelectedGenre { get; set; }
        public IEnumerable<AnimeDto> TaggedAnimes { get; set; }
    }
}