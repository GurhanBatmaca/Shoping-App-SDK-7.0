using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Identity;

namespace shopapp.webui.Controllers
{
    public class CartController: Controller
    {
        private readonly ICartService? cartService;
        private UserManager<ApplicationUser>? userManager;
        public CartController(ICartService? _cartService,UserManager<ApplicationUser>? _userManager)
        {
            cartService = _cartService;
            userManager = _userManager;
        }
        public async Task<IActionResult> AddToCart(int Id,int quantity)
        {
            var userId = userManager!.GetUserId(User);

            await cartService!.AddToCart(userId!,Id,quantity);

            return RedirectToAction("Index","Home");
        }
    }
}