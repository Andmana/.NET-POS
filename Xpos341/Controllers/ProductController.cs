using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class ProductController : Controller
    {
        private CategoryService categoryService;
        private VariantService variantService;
        private ProductService productService;

        private int idUser = 1;

        public ProductController(CategoryService _categoryService, 
                                 VariantService _variantService,
                                 ProductService _productService)
        {
            categoryService = _categoryService;
            variantService = _variantService;
            productService = _productService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
