using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMarket.Models;
using GameMarket.ViewModels;


namespace GameMarket.Controllers
{
    public class ShCartController : Controller
    {
        GameMarketDB marketDB = new GameMarketDB();
      

        public ActionResult Index()
        {
            var cart = ShCart.GetCart(marketDB, this.HttpContext);

         
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };


            return View(viewModel);
        }

        
        public ActionResult AddToCart(int id)
        {


            var addedGame = marketDB.Games
                .Single(game => game.GameId == id);

           
            var cart = ShCart.GetCart(marketDB, this.HttpContext);

            cart.AddToCart(addedGame);

            marketDB.SaveChanges();


            return RedirectToAction("Index");
        }

        

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
          
            var cart = ShCart.GetCart(marketDB, this.HttpContext);

           
            string gameName = marketDB.Carts
                .Single(item => item.NoteId == id).Game.Name;

           
            int itemCount = cart.RemoveFromCart(id);

            marketDB.SaveChanges();

            string removed = (itemCount > 0) ? " 1 copy of " : string.Empty;

           

            var results = new ShoppingCartRemoveViewModel
            {
                Message = removed + gameName +
                    "была удалена из вашей корзины.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShCart.GetCart(marketDB, this.HttpContext);

            var cartItems = cart.GetCartItems()
                .Select(a => a.Game.Name)
                .OrderBy(x => x);

            ViewBag.CartCount = cartItems.Count();
            ViewBag.CartSummary = string.Join("\n", cartItems.Distinct());

            return PartialView("CartSummary");
        }

    }
}