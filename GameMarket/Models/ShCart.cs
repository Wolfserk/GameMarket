using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameMarket.Models
{
    public class ShCart
    {
          GameMarketDB _db;
        string ShCartId { get; set; }

        public ShCart(GameMarketDB db)
        {
            _db = db;
        }

        public const string CartSessionKey = "CartId";

        public static ShCart GetCart(GameMarketDB db, HttpContextBase context)
        {
            var cart = new ShCart(db);
            cart.ShCartId = cart.GetCartId(context);
            return cart;
        }
        public static ShCart GetCart(GameMarketDB db, Controller controller)
        {
            return GetCart(db, controller.HttpContext);
        }

        public void AddToCart(Game game)
        {
            // Get the matching cart and album instances
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == ShCartId
                && c.GameId == game.GameId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    GameId = game.GameId,
                    CartId = ShCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _db.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
        }
             public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _db.Carts.Single(
                cart => cart.CartId == ShCartId
                && cart.NoteId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _db.Carts.Remove(cartItem);
                }

            }

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _db.Carts.Where(cart => cart.CartId == ShCartId);

            foreach (var cartItem in cartItems)
            {
                _db.Carts.Remove(cartItem);
            }

        }

        public List<Cart> GetCartItems()
        {
            return _db.Carts.Where(cart => cart.CartId == ShCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _db.Carts
                          where cartItems.CartId == ShCartId
                          select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _db.Carts
                              where cartItems.CartId == ShCartId
                              select (int?)cartItems.Count * cartItems.Game.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var album = _db.Games.Find(item.GameId);

                var orderDetail = new OrderDet
                {
                    GameId = item.GameId,
                    OrderId = order.OrderId,
                    PricePerGame = album.Price,
                    Count = item.Count,
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Game.Price);

                _db.OrderDets.Add(orderDetail);
            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var ShCart = _db.Carts.Where(c => c.CartId == ShCartId);

            foreach (Cart item in ShCart)
            {
                item.CartId = userName;
            }

        }
    }
}