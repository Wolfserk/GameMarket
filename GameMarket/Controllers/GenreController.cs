using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMarket.Models;

namespace GameMarket.Controllers
{
     [Authorize(Roles = "Administrator")]
    public class GenreController : Controller
    {
        private GameMarketDB db = new GameMarketDB();
        //
        // GET: /StoreManager/

        public ActionResult Index()
        {
            var genres = db.Genres;
            return View(genres.ToList());
        }
        public ActionResult Details(int id = 0)
        {
            Genre genres = db.Genres.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", genre.GenreId);
            return View(genre);
        }
        public ActionResult Edit(int id = 0)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            SelectList genres = new SelectList(db.Genres, "GenreId", "Name", genre.GenreId);
            ViewBag.Genres = genres;
            return View(genre);
        }
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SelectList genres = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.Genres = genres;
            return View(genre);
        }
        public ActionResult Delete(int id = 0)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
	}
}