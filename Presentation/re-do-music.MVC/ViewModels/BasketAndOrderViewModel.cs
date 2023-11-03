using ReDoMusic.Domain.Entites;
using ReDoMusic.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace re_do_music.MVC.ViewModels
{
    public class BasketAndOrderViewModel
    {
        public Basket Basket { get; set; }
        public AppUser User { get; set; }

        [Required(ErrorMessage = "Shipping Address is required.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        public Payment PaymentMethod { get; set; }
    }
}
