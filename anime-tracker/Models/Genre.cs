using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace anime_tracker.Models
{
    public class Genre
    {
        [Key]
        public int genre_id { get; set; }
        public string genre_name { get; set; }

        //genres can belong to more than one anime
        //public ICollection<Anime> Animes { get; set; }
    }
}