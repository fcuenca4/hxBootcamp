using MVC.Entities;
using MVC.Web.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Web.ModelConvertion
{
    public class MovieConversor
    {
        public static MovieVM toMovieVM(Movie movie) {
            if (movie == null)
                return null;
            MovieVM toR = new MovieVM
            {
                Id = movie.Id,
                Name = movie.Name,
                Genres = GenreConversor.toGenreVMs(movie.Genres.ToArray()),
                Actors = ActorConversor.toActorVMs(movie.Actors.ToArray()),
                Description = movie.Description,
                Duration = movie.Duration,
                Image = movie.Image,
                ReleaseDate = movie.ReleaseDate
            };
            return toR;
        }
       
        public static MovieVM[] toMovieVMs(Movie[] movies) {
            List<MovieVM> toR = new List<MovieVM>();
            if (movies == null)
                return toR.ToArray();
            foreach (var movie in movies)
            {
                toR.Add(new MovieVM { Id = movie.Id, Name = movie.Name, Image = movie.Image});
            }
            return toR.ToArray();
        }
    }
}