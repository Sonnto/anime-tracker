using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace anime_tracker.Models
{
    public class Anime
    {
        [Key] //online solution to "EntityType 'Anime' has no key defined. Define the key for this EntityType." error was to add "[Key]".
        public int anime_id { get; set; }
        public string anime_title { get; set; }
        [ForeignKey("AnimeTypes")] //An anime can have only one type of anime type.
        public int anime_type_id { get; set; }
        public virtual AnimeType AnimeTypes { get; set; }
        public int rating { get; set; }
        public int completed_episodes { get; set; }
        public int total_episodes { get; set; }
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string activity { get; set; }
        public bool favorite { get; set; }

        //anime can have multiple genres
        public ICollection<Genre> Genres { get; set; }
    }

    public class AnimeDto
        // simplified view of an anime
    {

        public int anime_id { get; set; }
        public string anime_title { get; set; }
        public int anime_type_id { get; set; } 
        public string anime_type_name { get; set; }
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string activity { get; set; }
        public int completed_episodes { get; set; }
        public int total_episodes { get; set; }
        public int rating { get; set; }
        public bool favorite { get; set; }
        public ICollection<Genre> Genres { get; set; }

    }
}