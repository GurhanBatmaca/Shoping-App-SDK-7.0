using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart>, ICartRepository
    {
        public EfCoreCartRepository(ShopContext context):base(context)
        {
            
        }
        private ShopContext? ShopContext
        { 
            get {return context as ShopContext;}  
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await ShopContext!.Carts
                                    .Include(i => i.CartItems!)
                                    .ThenInclude(i => i.Product!)
                                    .FirstOrDefaultAsync(i => i.UserId == userId);
        }


        public async Task InitializeCartAsync(string userId)
        {
            ShopContext!.Carts.Add(new Cart()
            {
                UserId = userId
            });

            await ShopContext.SaveChangesAsync();
        }
    }
}