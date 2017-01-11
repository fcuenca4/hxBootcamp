using MVC.Entities;
using MVC.Web.Models.Actor;
using MVC.Web.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Web.ModelConvertion
{
    public static class ActorConversor
    {
        public static ActorVM toActorVM(Actor a, ICollection<Movie> movies) {
            if (a == null)
                return null;
            List<MovieVM> mvs = new List<MovieVM>();
            if (movies != null)
                foreach (var m in movies)
                {
                    mvs.Add(new MovieVM { Id = m.Id, Name = m.Name });
                }
            return new ActorVM { Id = a.Id, Name = a.Name, UrlBiography = a.UrlBiography, Movies = mvs };
        }
        public static ActorVM[] toActorVMs(Actor[] actors) {
            List<ActorVM> toR = new List<ActorVM>();
            foreach (var m in actors)
            {
                toR.Add(new ActorVM { Id = m.Id, Name = m.Name });
            }
            return toR.ToArray();
        }
    }
}