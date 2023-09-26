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

        public async Task InitializeCart(string userId)
        {          
            await unitOfWork.Carts.InitializeCart(userId);
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Validation(Cart entity)
        {
            throw new NotImplementedException();
        }

        public async Task AddToCart(string userId,int productId, int quantity)
        {
            var cart = await GetByUserId(userId);

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

        public async Task<Cart?> GetByUserId(string userId)
        {
            return await unitOfWork.Carts.GetByUserId(userId);
        }
    }
}