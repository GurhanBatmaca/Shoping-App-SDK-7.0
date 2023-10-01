using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Extentions;
using shopapp.webui.Helpers;
using shopapp.webui.Identity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles ="Admin")]
    public class AdminController: Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IOrderService orderService;
        public AdminController(IProductService _productService,ICategoryService _categoryService,IOrderService _orderService)
        {
            productService = _productService;
            categoryService = _categoryService;
            orderService = _orderService;
        }

        public async Task<IActionResult> ProductList(int sayfa=1)
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

            var entity = new Product()
            {
                Name = model.Name,  
                Price = model.Price,
                Description = model.Description,
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

            
            if(model!.Photo == null)
            {
                var randomName = "noProductImage.png";
                entity.ImageUrl = randomName;

                TempData.Put("message",new InfoMessage
                {
                    Title = $"{entity.Name} İsimli ürün eklendi.",
                    Message = "Resim seçilmediği için varsayılan eklendi.",
                    AlertType = "success"
                });

            }
            else
            {
                var extention = Path.GetExtension(model.Photo!.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img", randomName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }
                entity.ImageUrl = randomName;
                
            }            
                
            await productService.CreateAsync(entity);

            TempData.Put("message",new InfoMessage
            {
                Title = $"{entity.Name} İsimli ürün eklendi.",
                AlertType = "success"
            });

            return RedirectToAction("ProductList"); 
                   
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity = await productService.GetByIdWithCategories((int)id!);

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

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model,int[] categoriesIds)
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

            var entity = await productService.GetByIdAsync(model.Id);

            if(model.Photo != null)
            {
                var extention = Path.GetExtension(model.Photo!.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img", randomName);

                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                if(entity!.ImageUrl != "noProductImage")
                {
                    var exPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\img",entity!.ImageUrl!);
                    System.IO.File.Delete(exPath);

                    entity.ImageUrl = randomName;
                }
                    entity.ImageUrl = randomName;

            }
            else
            {
                entity!.ImageUrl = entity!.ImageUrl;
            }
            
            entity!.Name = model.Name;
            entity.Price = model.Price;
            entity.Url = UrlModifier.Modifie(model.Name!);
            entity.Description = model.Description;
            entity.IsAproved = model.IsAproved;
            entity.IsHome = model.IsHome;
            entity.IsPopular = model.IsPopular;

            productService.Update(entity,categoriesIds);

            TempData.Put("message",new InfoMessage
            {
                Title = $"{entity.Name} İsimli ürün güncellendi.",
                AlertType = "warning"
            });
            
            return RedirectToAction("ProductList"); 
        }

        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var entity = await productService.GetByIdAsync((int)id);

            if(entity == null)
            {
                return NotFound();
            }

            productService.Delete(entity);

            TempData.Put("message",new InfoMessage
            {
                Title = $"{entity.Name} İsimli ürün silindi.",
                AlertType = "danger"
            });

            return RedirectToAction("ProductList");
        }
        
        public async Task<IActionResult> CategoryList()
        {
            var categories = await categoryService.GetAllAsync();

            var categoryListModel = new CategoryListModel()
            {
                Categories = categories
            };

            return View(categoryListModel);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategory()
        {
            ViewBag.Categories = await categoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryModel model)
        {
            

            if(!ModelState.IsValid)
            {
                ViewBag.Categories = await categoryService.GetAllAsync();
                return View();
            }

            var entity = new Category()
            {
                Name = model.Name,
                Url = UrlModifier.Modifie(model.Name!)
            };

            await categoryService.CreateAsync(entity);

            TempData.Put("message",new InfoMessage
            {
                Title = $"{entity.Name} İsimli kategori eklendi.",
                AlertType = "success"
            });

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity = await categoryService.GetByIdAsync((int)id);

            if(entity == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await categoryService.GetAllAsync();
            var categoryModel = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
            };

            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Categories = await categoryService.GetAllAsync();
                return View(model);
            }

            var entity = await categoryService.GetByIdAsync(model.Id);

            if(model == null)
            {
                return NotFound();
            }

            entity!.Name = model.Name;
            entity.Url = UrlModifier.Modifie(model.Name!);

            categoryService.Update(entity);

            TempData.Put("message",new InfoMessage
            {
                Title = $"{entity.Name} İsimli kategori güncellendi.",
                AlertType = "warning"
            });

            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity = await categoryService.GetByIdAsync((int)id!);

            if(entity == null)
            {
                return NotFound();
            }

            categoryService.Delete(entity);

            TempData.Put("message",new InfoMessage
            {
                Title = $"{entity.Name} İsimli kategori silindi.",
                AlertType = "danger"
            });

            return RedirectToAction("CategoryList");
        }
    
        public async Task<IActionResult> OrderList()
        {
            var orders = await orderService.GetAllOrdersAsync();

            var orderWiewModelList = new List<OrderViewModel>();

            var orderModel = new OrderViewModel();

            foreach (var order in orders)
            {

                orderModel = new OrderViewModel();

                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Address;
                orderModel.City = order.City;
                orderModel.OrderState = order.OrderState;
                orderModel.PaymentType = order.PaymentType;

                orderModel.OrderItems = order.OrderItems!.Select(i => new OrderItemViewModel(){
                    OrderItemId = i.Id,
                    Name = i.Product!.Name,
                    Price = (double)i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderWiewModelList.Add(orderModel);
            }

            return View(orderWiewModelList);
        }
    
        public async Task<IActionResult> UpdateOrder(int orderId)
        {
            return View(orderId);
        }
    }
}