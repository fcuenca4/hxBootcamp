using MVC.Web.Models.Actor;
using MVC.Web.Models.Genre;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Web.Models.Movie
{
    public class MovieVM : EntityBaseVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public String Image { get; set; }
        public IList<ActorVM> Actors { get; set; }
        [Required]
        public IList<GenreVM> Genres { get; set; }
        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }

        [DisplayName("Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Range(30,300)]
        public int Duration { get; set; }
    }
}