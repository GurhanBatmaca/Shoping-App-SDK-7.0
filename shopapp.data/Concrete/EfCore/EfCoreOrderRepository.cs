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

        public async Task<List<Order>> GetAllOrdersAsync(EnumOrderState orderState,int page,int pageSize)
        {
            var orders = ShopContext!.Orders
                                    .Include(i => i.OrderItems!)
                                    .ThenInclude(i=>i.Product)
                                    .AsQueryable();

            if(orderState == (EnumOrderState)1 || orderState == (EnumOrderState)2 || orderState == (EnumOrderState)3|| orderState == (EnumOrderState)4)
            {
                orders = orders
                        .Where(i => i.OrderState == orderState+1);
            };

            return await orders.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Order?> GetByIdWithItemsAsync(int orderId)
        {
            return await ShopContext!.Orders
                                            .Where(i => i.Id == orderId)
                                            .Include(i => i.OrderItems!)
                                            .ThenInclude(i=>i.Product)
                                            .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrdersAsync(string userId)
        {
            return await ShopContext!.Orders
                                    .Include(i => i.OrderItems!)
                                    .ThenInclude(i=>i.Product)
                                    .Where(i => i.UserId == userId)
                                    .ToListAsync();
        }

        public async Task UpdateStateAsync(int orderId, EnumOrderState orderState)
        {
            var entity = await ShopContext!.Orders
                                            .Where(i => i.Id == orderId)
                                            .FirstOrDefaultAsync();

            entity!.OrderState = orderState;

            await ShopContext.SaveChangesAsync();
        }
    }
}