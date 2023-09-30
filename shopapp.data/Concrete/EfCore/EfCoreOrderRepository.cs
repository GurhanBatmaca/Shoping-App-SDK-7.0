using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreOrderRepository: EfCoreGenericRepository<Order>,IOrderRepository
    {
        public EfCoreOrderRepository(ShopContext context):base(context)
        {
            
        }

        private ShopContext? ShopContext
        {
            get { return context as ShopContext; }
        }

        public async Task<List<Order>> GetOrdersAsync(string userId)
        {
            return await ShopContext!.Orders
                                    .Include(i => i.OrderItems!)
                                    .ThenInclude(i=>i.Product)
                                    .Where(i => i.UserId == userId && i.OrderState == 0)
                                    .ToListAsync();
        }
    }
}