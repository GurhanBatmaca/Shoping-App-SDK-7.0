using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;


namespace shopapp.business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork unitOfWork;
        public CartManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task InitializeCartAsync(string userId)
        {          
            await unitOfWork.Carts.InitializeCartAsync(userId);
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Validation(Cart entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddToCartAsync(string userId,int productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId);

            var index = cart!.CartItems!.FindIndex(i => i.ProductId == productId);

            if(index < 0)
            {
                cart.CartItems.Add(new CartItem() 
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cart.Id
                });
            }
            else
            {
                cart.CartItems[index].Quantity += quantity;
            }

            unitOfWork.Carts.Update(cart);
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await unitOfWork.Carts.GetCartByUserIdAsync(userId);
        }

        public async Task DeleteFromCartAsync(int cartId, int productId)
        {
            await unitOfWork.Carts.DeleteFromCartAsync(cartId,productId);
        }

        public async Task ClearCartAsync(int cartId)
        {
            await unitOfWork.Carts.ClearCartAsync(cartId);
        }
    }
}