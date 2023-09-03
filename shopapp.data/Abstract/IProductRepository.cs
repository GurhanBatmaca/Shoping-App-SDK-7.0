using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<List<Product>?> GetHomePageProducts();
        Task<List<Product>?> GetProductsByCategory(string category,int page,int pageSize);
        Task<int> GetProductsCountByCategory(string category);
        Task<Product?> GetProductDetails(string url);

        Task<List<Product>?> GetSearchResult(string q,int page,int pageSize);
        Task<int> GetProductsCountBySearch(string searchString);
    }
}