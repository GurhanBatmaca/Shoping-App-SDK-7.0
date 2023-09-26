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
    }
}