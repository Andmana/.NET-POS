using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class CategoryTryController : Controller
    {
        private readonly Xpos341Context db ;
        private readonly CategoryTryService categoryTryService;

        public CategoryTryController(Xpos341Context _db)
        {
            db = _db;
            this.categoryTryService = new CategoryTryService(db);
        }

        public IActionResult Index()
        {
            List<VMTblCategory> dataView = categoryTryService.GetAllData();
            return View(dataView);
        }
    }
}
