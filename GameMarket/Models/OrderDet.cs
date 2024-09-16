using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameMarket.Models
{
    public class OrderDet
    {
        public int OrderDetId { get; set; }
        public int OrderId { get; set; }
        public int GameId { get; set; }
        public int Count { get; set; }
        public decimal PricePerGame { get; set; }

        public virtual Game Game { get; set; }
        public virtual Order Order { get; set; }
    }
}