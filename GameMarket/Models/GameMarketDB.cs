using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GameMarket.Models
{
    public class GameMarketDB : DbContext
    {
        public GameMarketDB() :
            base("DefaultConnection")
        { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDet> OrderDets { get; set; }
    }
}