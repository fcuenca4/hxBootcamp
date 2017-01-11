using MVC.Web.Models.Movie;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Web.Models.Actor
{
    public class ActorVM:EntityBaseVM
    {
        public bool isSelected { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public IList<MovieVM> Movies { get; set; }
        
        [DisplayName("URL Biography")]
        [MaxLength(150)]
        public string UrlBiography { get; set; }
    }
}