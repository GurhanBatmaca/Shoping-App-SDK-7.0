using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;


namespace shopapp.business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task CreateAsync(Order entity)
        {
            await unitOfWork.Orders.CreateAsync(entity);
        }

        public async Task<List<Order>> GetInCompleteOrdersAsync(string userId)
        {
            return await unitOfWork.Orders.GetInCompleteOrdersAsync(userId);
        }

        public bool Validation(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}