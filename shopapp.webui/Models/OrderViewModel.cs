using System.ComponentModel.DataAnnotations;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class OrderViewModel 
    {
        
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? UserId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }

        public EnumPaymentType PaymentType { get; set; }
        public EnumOrderState OrderState { get; set; }

        public List<OrderItemViewModel>? OrderItems { get; set; } = new List<OrderItemViewModel>();

        public double TotalPrice()
        {
            return OrderItems!.Sum(i=>i.Price * i.Quantity);
        }
    }


}