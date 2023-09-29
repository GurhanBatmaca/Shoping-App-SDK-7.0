using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Helpers
{
    public static class ModelToEntity
    {
        public static Order OrderModelToOrderEntity(OrderModel model,string userId)
        {
            var order = new Order();

            order.OrderNumber = new Random().Next(111111,999999).ToString();
            order.OrderState = EnumOrderState.completed;
            order.PaymentType = EnumPaymentType.CrediCard;
            order.OrderDate = new DateTime();
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Address = model.Address;
            order.City = model.City;
            order.UserId = userId;
            order.Phone = model.Phone.ToString();
            order.Email = model.Email;
            order.Note = model.Note;
            order.ConversationId = model.ConversationId; 
            order.PaymentId = model.PaymentId;


            order.OrderItems = new List<OrderItem>();

            foreach (var item in model.CartModel!.CartItems!)
            {
                var orderItem = new OrderItem()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };

                order.OrderItems.Add(orderItem);
            }


            return order;
        }
    }
}