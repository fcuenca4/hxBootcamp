using System.Collections.Generic;

namespace MVC.Web.Models.Genre
{
    public class CreateGenreVM
    {
        public GenreVM newGenre { get; set; }
        public IEnumerable<GenreVM> allGenres { get; set; }
    }
}