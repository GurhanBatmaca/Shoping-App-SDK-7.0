using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;


namespace shopapp.webui.ViewComponents
{
    public class CategoriesViewComponent: ViewComponent
    {

        protected readonly ICategoryService categoryService;
        public CategoriesViewComponent(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(await categoryService.GetAllAsync());
        }
    }
}