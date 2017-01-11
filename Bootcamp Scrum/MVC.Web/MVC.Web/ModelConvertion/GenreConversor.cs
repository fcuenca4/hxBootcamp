using MVC.Entities;
using MVC.Web.Models.Genre;
using MVC.Web.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Web.ModelConvertion
{
    public static class GenreConversor
    {
        public static GenreVM toGenreVM(Genre g, ICollection<Movie> movies) {
            if(g==null)
                return null;
            List<MovieVM> mvs = new List<MovieVM>();
            if(movies != null)
                foreach (var m in movies)
                {
                    mvs.Add(new MovieVM { Id = m.Id, Name = m.Name});
                }
            return new GenreVM {Id = g.Id, Name = g.Name, Movies = mvs};
        }
        public static GenreVM[] toGenreVMs(Genre[] genres) {
            List<GenreVM> toR = new List<GenreVM>();
            foreach (var genre in genres)
            {
                toR.Add(new GenreVM { Id = genre.Id, Name = genre.Name});
            }
            return toR.ToArray();
        }

    }
}