using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Helpers;
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
                ModelState.AddModelError("", "Lütfen en az bir kategori seçin.");
                return View(model);
            }
                
            var extention = Path.GetExtension(model.Photo!.FileName);
            var randomName = string.Format($"{Guid.NewGuid()}{extention}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img", randomName);

            using (var stream = new FileStream(path,FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
            }


            var entity = new Product()
            {
                Name = model.Name,  
                Price = model.Price,
                Description = model.Description,
                ImageUrl = randomName,
                Url = UrlModifier.Modifie(model.Name!),
                IsAproved = model.IsAproved,
                IsHome = model.IsHome,
                IsPopular = model.IsPopular,
                ProductCategories = categoriesIds.Select(catId => new ProductCategory()
                {
                    ProductId = model.Id,
                    CategoryId = catId
                }).ToList()                             
            };

            await productService.CreateAsync(entity);

            return RedirectToAction("ProductsList"); 
                   
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var entity = await productService.GetByIdWithCategories(id);

            if(entity == null)
            {
                return  NotFound();   
            }

            var productModel = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description,
                IsAproved = entity.IsAproved,
                IsHome = entity.IsHome,
                IsPopular = entity.IsPopular,
                SelectedCategories = entity.ProductCategories!.Select(i => i.Category!).ToList()
            };

            ViewBag.Categories = await categoryService.GetAllAsync();

            return View(productModel);
        }

        
        
    }
}