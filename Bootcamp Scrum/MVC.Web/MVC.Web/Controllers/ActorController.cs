using MVC.Entities;
using MVC.Services.Interface;
using MVC.Web.ModelConvertion;
using MVC.Web.Models.Actor;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVC.Web.Controllers
{
    public class ActorController : Controller
    {
        private IActorService _ActorService;
        private IMovieService _MovieService;
        public ActorController(IActorService actorService, IMovieService movieService)
        {
            _ActorService = actorService;
            _MovieService = movieService;
        }
        // GET: Actor
        public ActionResult Index()
        {

            return View(ActorConversor.toActorVMs(_ActorService.GetAllElements().ToArray()));

        }
        // GET: Actor/Details/5
        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ActorVM actor = ActorConversor.toActorVM(
                _ActorService.GetElement(id.Value),
                _MovieService.FindElements(m=>m.Actors.Where(a=>a.Id == id.Value).Count()>0));
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);

        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create(ActorVM act)
        {
            try
            {
                Actor actor = new Actor
                {
                    Name = act.Name,
                    UrlBiography = act.UrlBiography
                };
                _ActorService.SaveElement(actor);
                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                return View(act);
            }
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ActorVM act = ActorConversor.toActorVM(
                _ActorService.GetElement(id.Value),
                null
            );
            if (act == null)
                return HttpNotFound();
            return View(act);
        }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(ActorVM actor)
        {
            try
            {
                Actor a = _ActorService.GetElement(actor.Id);
                if (a == null)
                    return HttpNotFound();
                a.Name = actor.Name;
                a.UrlBiography = actor.UrlBiography;
                _ActorService.UpdateElement(a);
                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                return View(actor);
            }
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ActorVM a = ActorConversor.toActorVM(_ActorService.GetElement(id.Value), null);
            if (a == null)
                return HttpNotFound();
            return View(a);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Actor a = _ActorService.GetElement(id.Value);
                if (a == null)
                    return HttpNotFound();
                _ActorService.RemoveElement(a);
                return RedirectToAction("Index", "Movie");
            }
            catch
            {
                return RedirectToAction("Details", id);
            }
        }
    }
}
