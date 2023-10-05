using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Extentions;
using shopapp.webui.Helpers;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class OrderController: Controller
    {
        private readonly ICartService? cartService;
        private readonly IOrderService? orderService;
        private readonly UserManager<ApplicationUser>? userManager;
        private readonly IConfiguration configuration;
        public OrderController(ICartService? _cartService,IOrderService? _orderService,UserManager<ApplicationUser>? _userManager,IConfiguration _configuration)
        {
            cartService = _cartService;
            orderService = _orderService;
            userManager = _userManager;
            configuration = _configuration;
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = userManager!.GetUserId(User);
            var cart = await cartService!.GetCartByUserIdAsync(userId!);

            var orderModel = new OrderModel();

            orderModel.CartModel = new CartModel()
            {
                CartId = cart!.Id,
                CartItems = cart.CartItems!.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product!.Name,
                    Price = (double)i.Product.Price!,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            };


            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel model)
        {

            var userId = userManager!.GetUserId(User);
            var cart = await cartService!.GetCartByUserIdAsync(userId!);

            model.CartModel = new CartModel()
            {
                CartId = cart!.Id,
                CartItems = cart.CartItems!.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product!.Name,
                    Price = (double)i.Product.Price!,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity

                }).ToList()
            };

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var payment = PaymentProcess.PayWithIyzipay(model,configuration);

            if(payment.Status == "success")
            {
                TempData.Put("message",new InfoMessage {
                    Message= "Ödeme başarılı",
                    AlertType ="success"
                });

                model.PaymentId = payment.PaymentId;
                model.ConversationId = payment.ConversationId;

                var entity = ModelToEntity.OrderModelToOrderEntity(model,userId!);

                await orderService!.CreateAsync(entity);
                await cartService.ClearCartAsync(cart.Id);         

                return RedirectToAction("Orders");
            }

            else 
            {
                TempData.Put("message",new InfoMessage {
                    Message= $"Ödeme başarısız: {payment.ErrorMessage}.",
                    AlertType ="danger"
                });
                
                return View(model);
            }
            
        }
        public async Task<IActionResult> Orders(int sayfa=1)
        {
            const int pageSize = 2;
            var userId = userManager!.GetUserId(User);
            var orders = await orderService!.GetOrdersAsync(userId!,sayfa,pageSize);
            var ordersCount = await orderService.GetOrdersCountAsync(userId!);
            
            var orderList = new List<OrderViewModel>();
            var orderModel = new OrderViewModel();

            foreach (var order in orders)
            {
                orderModel = new OrderViewModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Address;
                orderModel.City = order.City;
                orderModel.OrderState = order.OrderState;
                orderModel.PaymentType = order.PaymentType;

                orderModel.OrderItems = order.OrderItems!.Select(i => new OrderItemViewModel(){
                    OrderItemId = i.Id,
                    Name = i.Product!.Name,
                    Price = (double)i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderList.Add(orderModel);
            }

            var orderListWiewModel = new OrderListWiewModel
            {
                OrderViewModels = orderList,
                PageInfo = new PageInfo
                {
                    CurrentPage = sayfa,
                    ItemPerPage = pageSize,
                    TotalItems = ordersCount,
                }
            };

            return View(orderListWiewModel);
        }

    }
}