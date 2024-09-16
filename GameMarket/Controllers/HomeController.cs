using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMarket.Models;

namespace GameMarket.Controllers
{
    public class HomeController : Controller
    {
        private GameMarketDB marketDB = new GameMarketDB();
        public ActionResult Index()
        {
            // ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var games = GetTopSellingGames(7);
            return View(games);
        }

        private List<Game> GetTopSellingGames(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return marketDB.Games
                .OrderByDescending(a => a.OrderDets.Count())
                .Take(count)
                .ToList();
        }
    

        public ActionResult Contact()
         {
             ViewBag.Message = "Your contact page.";

             return View();
         }
         [Authorize(Roles = "Administrator")]
        public ActionResult ADM()
        {
            return View();
        }

    }
}
