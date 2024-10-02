using Microsoft.AspNetCore.Mvc;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        private int idUser = 1;

        public CategoryController(CategoryService _categoryService)
        {
            categoryService = _categoryService;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
