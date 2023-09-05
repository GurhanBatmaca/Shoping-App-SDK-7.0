using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class AdminController: Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public AdminController(IProductService _productService,ICategoryService _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {

            ViewBag.Categories = await categoryService.GetAllAsync();

            return View();            
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model,int[] categoriesIds)
        {


            if(!ModelState.IsValid)
            {
                ViewBag.Categories = await categoryService.GetAllAsync();
                return View(model);
                
            }
            if(categoriesIds.Length <= 0)
            {
                ViewBag.Categories = await categoryService.GetAllAsync();
                ModelState.AddModelError("", "Kategori Hatası.");
                return View(model);
            }
                
            return RedirectToAction("ProductsList"); 
                   
        }

        public async Task<IActionResult> ProductsList(int sayfa=1)
        {
            const int pageSize = 3;

            var products = await productService.GetAllProductsByPage(sayfa, pageSize);
            var productsCount = await productService.CountAsync();

            var productListModel = new ProductListModel()
            {
                Products = products,
                PageInfo = new PageInfo()
                {
                    TotalItems = productsCount,
                    ItemPerPage = pageSize,
                    CurrentPage = sayfa
                }
            };
            
            return View(productListModel);

          
        }
        
        
    }
}