using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICartService: IValidator<Cart>
    {
        Task InitializeCart(string userId);
        Task<Cart?> GetByUserId(string userId);
        Task AddToCart(string userId,int productId,int quantity);
    }
}