using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace anime_tracker.Models
{
    public class AnimeXGenre
    {
        [Key]
        public int animexgenre_id { get; set; }
        [ForeignKey("Animes")]
        public int anime_id { get; set; }
        public virtual Anime Animes { get; set; }
        [ForeignKey("Genres")]
        public int genre_id { get; set; }
        public virtual Genre Genres { get; set; }
    }

    public class AnimeXGenreDto
    {
        public int animexgenre_id { get; set; }
        public int anime_id { get; set; }
        public string genre_name { get; set; }
    }
}