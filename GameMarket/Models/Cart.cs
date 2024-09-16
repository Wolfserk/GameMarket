using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GameMarket.Models
{
    public class Cart
    {
        [Key]
        public int NoteId { get; set; }
        public string CartId { get; set; }
        public int GameId { get; set; }
        public int Count { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public virtual Game Game { get; set; }
    }
}