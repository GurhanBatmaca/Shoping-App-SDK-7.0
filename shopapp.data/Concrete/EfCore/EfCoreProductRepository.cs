using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreProductRepository: EfCoreGenericRepository<Product>,IProductRepository
    {
        public EfCoreProductRepository(ShopContext context): base(context)
        {           
        }

        private ShopContext? ShopContext
        { 
            get {return context as ShopContext;}  
        }

        public async Task<List<Product>?> GetHomePageProducts()
        {            
            return await ShopContext!.Products.Where(i=>i.IsHome && i.IsAproved).ToListAsync();
        }

    }
}