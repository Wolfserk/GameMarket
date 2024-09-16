using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameMarket.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [DisplayName("Жанр:")]
        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}