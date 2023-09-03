using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IProductService: IValidator<Product>
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task CreateAsync(Product entity);
        void Update(Product entity);
        void Delete(Product entity);

        Task<List<Product>?> GetHomePageProducts();
    }
}