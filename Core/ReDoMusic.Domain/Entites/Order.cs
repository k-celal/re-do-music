using ReDoMusic.Domain.Common;
using ReDoMusic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Domain.Entites
{
    public class Order : EntityBase<Guid>
    {
        public AppUser Customer { get; set; }
        public string ShippingAddress { get; set; }
        public Payment PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }

        // Siparişe ait öğelerin listesi (BasketItem)
        public Basket OrderItems { get; set; }

        // Siparişi veren kullanıcının kimliği
    }

}

