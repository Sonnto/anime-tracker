using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace anime_tracker.Models
{
    public class AnimeType
    {
        [Key]
        public int anime_type_id { get; set; }
        public string anime_type_name { get; set; }
    }
}