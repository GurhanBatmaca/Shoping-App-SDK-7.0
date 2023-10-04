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

        public async Task<List<Order>> GetAllOrdersAsync(string category,EnumOrderState orderState,int page,int pageSize)
        {
            return await unitOfWork.Orders.GetAllOrdersAsync(category,orderState,page,pageSize);
        }

        public async Task<int> GetAllOrdersCountAsync(string category, EnumOrderState orderState)
        {
            return await unitOfWork.Orders.GetAllOrdersCountAsync(category,orderState);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await unitOfWork.Orders.GetByIdAsync(id);
        }

        public async Task<Order?> GetByIdWithItemsAsync(int orderId)
        {
            return await unitOfWork.Orders.GetByIdWithItemsAsync(orderId);
        }

        public async Task<List<Order>> GetOrdersAsync(string userId)
        {
            return await unitOfWork.Orders.GetOrdersAsync(userId);
        }

        public async Task UpdateStateAsync(int orderId, EnumOrderState orderState)
        {
            await unitOfWork.Orders.UpdateStateAsync(orderId,orderState);
        }

        public bool Validation(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}