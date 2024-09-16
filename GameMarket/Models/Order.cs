using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameMarket.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required]
        [DisplayName("Имя")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Фамилия")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Адрес")]
        [StringLength(70, MinimumLength = 3)]
        public string Address { get; set; }

        [Required]
        [DisplayName("Город")]
        [StringLength(40)]
        public string City { get; set; }

        [Required]
        [DisplayName("Область")]
        [StringLength(40)]
        public string State { get; set; }

        [Required]
        [DisplayName("Индекс")]
        [StringLength(10, MinimumLength = 5)]
        public string Index { get; set; }

        [Required]
        [DisplayName("Страна")]
        [StringLength(40)]
        public string Country { get; set; }

        [Required]
        [DisplayName("Номер телефона")]
        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Вы ввели неверный Email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrderDet> OrderDets { get; set; }
    }
}