using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>?> GetHomePageProducts();
    }
}