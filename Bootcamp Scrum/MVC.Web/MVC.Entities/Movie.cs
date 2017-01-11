using System;
using System.Collections.Generic;

namespace MVC.Entities
{
    public class Movie:EntityBase
    {
        public String Image { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public string Description { get; set; }

        public System.DateTime ReleaseDate { get; set; }

        public int Duration { get; set; }

    }
}