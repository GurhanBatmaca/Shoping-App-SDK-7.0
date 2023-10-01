using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class OrderItemViewModel  
    {
        public int OrderItemId { get; set; }
        public double Price { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
    }

}