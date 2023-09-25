using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.webui.Extentions;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers;

[AutoValidateAntiforgeryToken]
public class HomeController : Controller
{
    private readonly IProductService productService;

    public HomeController(IProductService _productService)
    {
        productService = _productService;
    }

    public async Task<IActionResult> Index()
    {

        var product = await productService.GetHomePageProducts();
        
        var productsListModel = new ProductListModel()
        {
            Products = product
        };

        return View(productsListModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
