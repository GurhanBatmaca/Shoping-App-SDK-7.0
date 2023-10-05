using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CartController: Controller
    {
        private readonly ICartService? cartService;
        private readonly UserManager<ApplicationUser>? userManager;
        public CartController(ICartService? _cartService,UserManager<ApplicationUser>? _userManager)
        {
            cartService = _cartService;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = userManager!.GetUserId(User);

            var cart = await cartService!.GetCartByUserIdAsync(userId!);

            var cartModel = new CartModel()
            {
                CartId = cart!.Id,
                CartItems = cart.CartItems!.Select( i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    Name = i.Product!.Name,
                    Price = (double)i.Product.Price!,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            };

            return View(cartModel);
        }
        public async Task<IActionResult> AddToCart(int Id,int quantity)
        {
            var userId = userManager!.GetUserId(User);

            await cartService!.AddToCartAsync(userId!,Id,quantity);

            return RedirectToAction("Index");
        }
    
        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            var userId = userManager!.GetUserId(User);
            var cart = await cartService!.GetCartByUserIdAsync(userId!);

            await cartService.DeleteFromCartAsync(cart!.Id,productId);
            return RedirectToAction("Index");
        }
    }
}