using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public List<CartItemModel>? CartItems { get; set; } = new List<CartItemModel>();

        public double TotalPrice()
        {
            return CartItems!.Sum(i => i.Price * i.Quantity);
        }
    }
}