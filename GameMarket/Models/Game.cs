using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameMarket.Models
{
    public class Game
    {
        [ScaffoldColumn(false)]
        public int GameId { get; set; }
        public int GenreId { get; set; }
        [Required]
        [DisplayName("Название:")]
        [StringLength(160, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [DisplayName("Цена:")]
        [Range(0.1, 3000.00)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DisplayName("Путь до картинки:")]
        [StringLength(1024)]
        public string GameImgUrl { get; set; }

        [DisplayName("Автор:")]
        [StringLength(1024)]
        public string Author { get; set; }

        [DisplayName("Системные требования:")]
        [StringLength(1024)]
        public string SysRec { get; set; }

        [DisplayName("Описание:")]
        [StringLength(1024)]
        public string About { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual List<OrderDet> OrderDets { get; set; }
    }
}