using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICartService: IValidator<Cart>
    {
        Task InitializeCart(string userId);
    }
}