using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ShopController: Controller
    {
        private readonly IProductService productService;

        public ShopController(IProductService _productService)
        {
            productService = _productService;
        }
        public async Task<IActionResult> List(string kategori,int sayfa=1)
        {
            const int pageSize = 4;

            var products = await productService.GetProductsByCategory(kategori,sayfa,pageSize);

            var productsCount = await productService.GetProductsCountByCategory(kategori);
        
            var productsListModel = new ProductListModel()
            {
                Products = products,

                PageInfo = new PageInfo()
                {
                    TotalItems = productsCount,
                    CurrentPage = sayfa,
                    ItemPerPage = pageSize,
                    CurrentCategory = kategori
                }
            };

            return View(productsListModel);
        }

        public async Task<IActionResult> Details(string url)
        {
            if(url == null)
            {
                return NotFound();
            }

            Product? product = await productService.GetProductDetails(url);

            if(product == null)
            {
                return NotFound();
            }

            var productDetailsModel = new ProductDetailsModel()
            {
                Product = product,
                Categories = product.ProductCategories!.Select(i=>i.Category!).ToList()
            };            

            return View(productDetailsModel);
        }
    
        public async Task<IActionResult> Search(string q,int sayfa=1)
        {
            if(string.IsNullOrEmpty(q))
            {
                return RedirectToAction("Index","Home");
            }

            const int pageSize = 2;

            var products = await productService.GetSearchResult(q,sayfa,pageSize);

            var productsCount = await productService.GetProductsCountBySearch(q);

            var productsListModel = new ProductListModel()
            {
                Products = products,
                PageInfo = new PageInfo()
                {
                    TotalItems = productsCount,
                    CurrentPage = sayfa,
                    ItemPerPage = pageSize,
                    SearchString = q
                }
            };

            return View(productsListModel);
        }

        public async Task<IActionResult> Popular(int sayfa=1)
        {
            const int pageSize = 2;

            var products = await productService.GetPopularProducts(sayfa,pageSize);

            var productsCount = await productService.GetProductsCountByPopular();

            var productsListModel = new ProductListModel()
            {
                Products = products,
                PageInfo = new PageInfo()
                {
                    TotalItems = productsCount,
                    CurrentPage = sayfa,
                    ItemPerPage = pageSize
                }
            };

            return View(productsListModel);
        }

    }
}