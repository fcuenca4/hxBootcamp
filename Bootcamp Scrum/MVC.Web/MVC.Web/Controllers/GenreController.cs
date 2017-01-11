using MVC.Entities;
using MVC.Services.Interface;
using MVC.Web.ModelConvertion;
using MVC.Web.Models.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC.Web.Controllers
{
    public class GenreController : Controller
    {
        // Genre Service instance required
        private IGenreService _GenreService;
        private IMovieService _MovieService;
        public GenreController(IGenreService genreService, IMovieService movieService)
        {
            _GenreService = genreService;
            _MovieService = movieService;
        }
        public ActionResult Index()
        {
            return View(GenreConversor.toGenreVMs(_GenreService.GetAllElements().ToArray()));
        }
        [HttpGet]
        //GET: Movie/Find/
        public ActionResult Index(string genre_query)
        {
            var genresToRender = GenreConversor.toGenreVMs(_GenreService.GetAllElements().ToArray());
            if (!String.IsNullOrEmpty(genre_query) && !String.IsNullOrWhiteSpace(genre_query))
            {
                var genresToRenderFiltered = genresToRender.Where(x => x.Name.ToUpper().Contains(genre_query.ToUpper())).ToArray();
                if (genresToRender.Count() != 0)
                    return View(genresToRenderFiltered);
                else
                    return View(genresToRender);
            }
            else
                return View(genresToRender);
        }
        // GET: Genre/Details/5
        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            GenreVM genre = GenreConversor.toGenreVM(
                _GenreService.GetElement(id.Value),
                _MovieService.FindElements(m => m.Genres.Where(g => g.Id == id.Value).Count() > 0)
            );
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View(new CreateGenreVM
            {
                allGenres = GenreConversor.toGenreVMs(
                    _GenreService.GetAllElements().ToArray())
            });
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(GenreVM newGenre)
        {
            try
            {
                Genre g = new Genre {
                    Id = newGenre.Id,
                    Name = newGenre.Name
                };
                _GenreService.SaveElement(g);
                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                return View(new CreateGenreVM
                {
                    newGenre = newGenre,
                    allGenres = GenreConversor.toGenreVMs(
                    _GenreService.GetAllElements().ToArray())
                });
            }
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            GenreVM genre = GenreConversor.toGenreVM(_GenreService.GetElement(id.Value), null);
            if (genre == null)
                return HttpNotFound();
            return View(genre);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(GenreVM genre)
        {
            try
            {
                Genre g = _GenreService.GetElement(genre.Id);
                if (g == null)
                    return HttpNotFound();
                g.Name = genre.Name;
                _GenreService.UpdateElement(g);
                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            GenreVM genre = GenreConversor.toGenreVM(_GenreService.GetElement(id.Value), null);
            if (genre == null)
                return HttpNotFound();
            return View(genre);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Genre genre = _GenreService.GetElement(id.Value);
                if (genre == null)
                    return HttpNotFound();
                _GenreService.RemoveElement(genre);
                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                return RedirectToAction("Details", id);
            }
        }
        public ActionResult GetGenres(string term)
        {
            ICollection<Genre> genres = _GenreService.FindElements(g => g.Name.ToUpper().Contains(term.ToUpper()));
            IEnumerable<String> result = genres.Select(g => g.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
