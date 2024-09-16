using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMarket.Models;

namespace GameMarket.Controllers
{
    public class StoreController : Controller
    {
        GameMarketDB marketDB = new GameMarketDB();
        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = marketDB.Genres.ToList();

            return View(genres);
        }
        public ActionResult Browse(string genre)
        {
            var genreModel = marketDB.Genres.Include("Games")
                .Single(g => g.Name == genre);

            return View(genreModel);
        }
        public ActionResult Details(int id)
        {
            var game = marketDB.Games.Find(id);

            return View(game);
        }
        [ChildActionOnly]
        public ActionResult GradeMenu()
        {
            var genres = marketDB.Genres
                .OrderByDescending(
                    g => g.Games.Sum(
                    a => a.OrderDets.Sum(
                    od => od.Count)))
                .Take(9)
                .ToList();

            return PartialView(genres);
        }
    }
}