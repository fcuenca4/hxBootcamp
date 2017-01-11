using MVC.Entities;
using MVC.Services.Interface;
using MVC.Web.ModelConvertion;
using MVC.Web.Models.Actor;
using MVC.Web.Models.Genre;
using MVC.Web.Models.Movie;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC.Web.Controllers
{
    public class MovieController : Controller
    {
        private IActorService _ActorService;
        private IMovieService _MovieService;
        private IGenreService _GenreService;
        public MovieController(IMovieService movieService, IGenreService genreService, IActorService actorService)
        {
            _ActorService = actorService;
            _MovieService = movieService;
            _GenreService = genreService;
        }
        // GET: Movie
        public ActionResult Index()
        {
            return View(MovieConversor.toMovieVMs(_MovieService.GetAllElements().ToArray()));
        }

        // GET: Movie/Details/5
        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            MovieVM movie = MovieConversor.toMovieVM(_MovieService.GetElement(id.Value));
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            MovieVM model = new MovieVM
            {
                Genres = GenreConversor.toGenreVMs(_GenreService.GetAllElements().ToArray()),
                Actors = new List<ActorVM>()
            };
            return View(model);
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(MovieVM model, HttpPostedFileBase ImageInput)
        {
            try
            {
                ViewBag.ErrorMessage = "";
                if (ModelState.IsValid)
                {
                    ICollection<Genre> modelGenres = new List<Genre>();
                    foreach (GenreVM genreModel in model.Genres)
                    {
                        if (genreModel.isSelected)
                            modelGenres.Add(_GenreService.GetElement(genreModel.Id));
                    }
                    if (modelGenres.Count() == 0)
                    {
                        ViewBag.ErrorMessage = "At least one genre must be selected";
                        return View(model);
                    }
                    ICollection<Actor> modelActors = new List<Actor>();
                    if (model.Actors != null)
                    {
                        foreach (ActorVM actorModel in model.Actors)
                        {
                            var query = from act in _ActorService.GetAllElements()
                                        where act.Name.Equals(actorModel.Name)
                                        select act.Id;
                            Actor actor = null;
                            if (query.Count() > 0)
                                actor = _ActorService.GetElement(query.First());
                            else if (!actorModel.Name.Trim(' ').Equals(""))
                            {
                                actor = new Actor { Name = actorModel.Name, UrlBiography = "" };
                                actor = _ActorService.SaveElement(actor);
                            }
                            if (!modelActors.Contains(actor))
                                modelActors.Add(actor);
                        }
                    }
                    Movie movie = new Movie
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Duration = model.Duration,
                        ReleaseDate = model.ReleaseDate.Date,
                        Image = "default.png",
                        Actors = modelActors,
                        Genres = modelGenres
                    };
                    if (ImageInput != null && ImageInput.ContentLength > 0)
                    {
                        string pic, path;
                        do
                        {
                            pic = new Random().Next().ToString() + ImageInput.FileName;
                            path = Path.Combine(Server.MapPath("~/Content/Images"), pic);
                        } while (System.IO.File.Exists(path));
                        ImageInput.SaveAs(path);
                        movie.Image = pic;
                    }
                    _MovieService.SaveElement(movie);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Movie already exist.";
                return View(model);
            }
            
        }

        // GET: Movie/Create
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Guid? id)
        {
            ViewBag.ErrorMessage = "";
            Movie movie = this._MovieService.GetElement(id);
            MovieVM toReturn = MovieConversor.toMovieVM(movie);
            foreach (var act in toReturn.Actors)
            {
                act.isSelected = true;
            }
            toReturn.Genres = GenreConversor.toGenreVMs(_GenreService.GetAllElements().ToArray());
            foreach (var genre in toReturn.Genres)
            {
                var query = from c in movie.Genres
                            where c.Id.Equals(genre.Id)
                            select c;
                if (query.Count() > 0)
                    genre.isSelected = true;
            }
            return View(toReturn);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieVM model, HttpPostedFileBase ImageInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Movie movie = _MovieService.GetElement(model.Id);

                    foreach (GenreVM genreModel in model.Genres)
                    {
                        Genre g = _GenreService.GetElement(genreModel.Id);
                        if (!genreModel.isSelected && movie.Genres.Contains(g))
                            movie.Genres.Remove(g);
                        if(genreModel.isSelected && !movie.Genres.Contains(g))
                            movie.Genres.Add(g);
                    }
                    if (movie.Genres.Count() == 0)
                    {
                        ViewBag.ErrorMessage = "At least one genre must be selected";
                        return View(model);
                    }
                        
                    if (model.Actors != null)
                    {
                        foreach (ActorVM actorModel in model.Actors)
                        {
                            Actor a = _ActorService.GetElement(actorModel.Id);
                            if (!actorModel.isSelected && movie.Actors.Contains(a))
                                movie.Actors.Remove(a);
                        }
                    }
                    movie.Name = model.Name;
                    movie.Description = model.Description;
                    movie.Duration = model.Duration;
                    movie.ReleaseDate = model.ReleaseDate.Date;
                    if (ImageInput != null && ImageInput.ContentLength > 0)
                    {
                        string pic, path;
                        do
                        {
                            pic = new Random().Next().ToString() + ImageInput.FileName;
                            path = Path.Combine(Server.MapPath("~/Content/Images"), pic);
                        } while (System.IO.File.Exists(path));
                        ImageInput.SaveAs(path);
                        movie.Image = pic;
                    }
                    _MovieService.UpdateElement(movie);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "A movie with the same name already exist.";
                return View(model);
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            MovieVM movie = MovieConversor.toMovieVM(_MovieService.GetElement(id.Value));
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Movie movie = _MovieService.GetElement(id.Value);
            if (movie == null)
                return HttpNotFound();
            _MovieService.RemoveElement(movie);
            return RedirectToAction("Index");
        }
        // POST: Movie/Autocomplete/5
        public ActionResult GetActors(string term)
        {
            ICollection<Actor> actors = _ActorService.FindElements(a => a.Name.Contains(term));
            IEnumerable<String> result = actors.Select(a => a.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        //GET: Movie/Find/
        public ActionResult Index(string query)
        {
            ViewBag.Search = "";
            var moviesToRender = MovieConversor.toMovieVMs(_MovieService.GetAllElements().ToArray());
            if (!String.IsNullOrEmpty(query) && !String.IsNullOrWhiteSpace(query))
            {
                ViewBag.Search = query;
                var moviesToRenderFiltered = moviesToRender.Where(x=> x.Name.ToUpper().Contains(query.ToUpper())).ToArray();
                if (moviesToRender.Count() != 0 )
                    return View(moviesToRenderFiltered);
                else
                    return View(moviesToRender);
            }
            else
                return View(moviesToRender);
            //return Json("empty", JsonRequestBehavior.AllowGet);

        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
