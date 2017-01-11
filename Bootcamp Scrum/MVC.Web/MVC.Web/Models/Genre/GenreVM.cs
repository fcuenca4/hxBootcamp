using MVC.Web.Models.Movie;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Web.Models.Genre
{
    public class GenreVM : EntityBaseVM
    {
        public bool isSelected { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public IList<MovieVM> Movies { get; set; }
    }
}