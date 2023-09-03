using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;


namespace shopapp.business.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IUnitOfWork unitOfWork;
        public ProductManager(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        

        public async Task CreateAsync(Product entity)
        {
            await unitOfWork.Products.CreateAsync(entity);
        }

        public void Delete(Product entity)
        {
            unitOfWork.Products.Delete(entity);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await unitOfWork.Products.GetByIdAsync(id);
        }

        public void Update(Product entity)
        {
            unitOfWork.Products.Update(entity);
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Validation(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>?> GetHomePageProducts()
        {
            return await unitOfWork.Products.GetHomePageProducts();
        }
    }
}