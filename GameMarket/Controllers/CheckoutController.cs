using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMarket.Models;

namespace GameMarket.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        GameMarketDB storeDB = new GameMarketDB();
       const string PromoCode = "FREE";
        
       
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    
                    storeDB.Orders.Add(order);

                  
                    var cart = ShCart.GetCart(storeDB, this.HttpContext);
                    cart.CreateOrder(order);

                  
                    storeDB.SaveChanges();

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }

            }
            catch
            {
                
                return View(order);
            }
        }



        public ActionResult Complete(int id)
        {
          
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}