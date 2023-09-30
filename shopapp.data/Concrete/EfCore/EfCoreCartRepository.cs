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

        public async Task ClearCartAsync(int cartId)
        {
            var cartItems = await ShopContext!.CartItems       
                                        .Where(i => i.CartId == cartId)
                                        .ToListAsync();

            ShopContext.CartItems.RemoveRange(cartItems);

            await ShopContext.SaveChangesAsync();
        }


        public async Task DeleteFromCartAsync(int cartId,int productId)
        {
            var entity = await ShopContext!.CartItems
                                .Where(i=> i.CartId == cartId && i.ProductId == productId)
                                .FirstOrDefaultAsync();

            ShopContext!.CartItems.Remove(entity!);

            await ShopContext.SaveChangesAsync();

                                
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