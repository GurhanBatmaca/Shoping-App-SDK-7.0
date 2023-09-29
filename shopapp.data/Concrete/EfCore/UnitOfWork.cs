using shopapp.data.Abstract;

namespace shopapp.data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext? context;
        public UnitOfWork(ShopContext? _context)
        {
            context = _context;
        }
        private EfCoreProductRepository? productRepository;
        private EfCoreCategoryRepository? categoryRepository;
        private EfCoreCartRepository? cartRepository;
        private EfCoreOrderRepository? orderRepository;

        public IProductRepository Products => productRepository = productRepository! ?? new  EfCoreProductRepository(context!);

        public ICategoryRepository Categories => categoryRepository = categoryRepository! ?? new EfCoreCategoryRepository(context!);

        public ICartRepository Carts => cartRepository = cartRepository! ?? new EfCoreCartRepository(context!);

        public IOrderRepository Orders => orderRepository = orderRepository! ?? new EfCoreOrderRepository(context!);

        public void Dispose()
        {
            context!.Dispose();
        }

    }
}