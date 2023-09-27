using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class OrderController: Controller
    {
        private readonly ICartService? cartService;
        private readonly UserManager<ApplicationUser>? userManager;
        public OrderController(ICartService? _cartService,UserManager<ApplicationUser>? _userManager)
        {
            cartService = _cartService;
            userManager = _userManager;
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
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata");
                return View(model);
            }
            return View(model);
        }
    }
}