using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class VariantTryController : Controller
    {
        private readonly Xpos341Context db;
        private readonly CategoryTryService categoryTryService;
        private readonly VariantTryService variantTryService;
        private static VMPage page = new VMPage();
        

        public VariantTryController(Xpos341Context _db)
        {
            db = _db;
            categoryTryService = new CategoryTryService(db);
            variantTryService = new VariantTryService (db);

        }
        public IActionResult Index(VMPage pg)
        {
            ViewBag.idSort = string.IsNullOrEmpty(pg.sortOrder) ? "id_desc" : "";
            ViewBag.nameSort = pg.sortOrder == "name" ? "name_desc" : "name";
            ViewBag.currentSort = pg.sortOrder;
            ViewBag.currentShowData = pg.showData;

            if (pg.searchString != null) { 
                pg.pageNumber = 1;
            } else
            {
                pg.searchString = pg.currentFilter;
            }

            ViewBag.currentFilter = pg.currentFilter;

            List<VMTblVariant> dataView = variantTryService.GetAllData();

            if (!string.IsNullOrEmpty(pg.searchString))
            {
                   dataView = dataView.Where(a => a.NameVariant.ToLower() == pg.searchString.ToLower()
                                               || a.NameCategory.ToLower() == pg.searchString.ToLower())
                                      .ToList();
            }

            switch (pg.sortOrder)
            {
                case "name_desc":
                    dataView = dataView.OrderByDescending(a => a.NameVariant).ToList(); 
                    break;
                case "name":
                    dataView = dataView.OrderBy(a => a.NameVariant).ToList();
                    break;
                case "id_desc":
                    dataView = dataView.OrderByDescending(a => a.Id).ToList();
                    break;
                default:
                    dataView = dataView.OrderBy(a => a.Id).ToList();
                    break;
            }

            int pageSize = pg.showData ?? 3;
            page = pg;

            return View(PaginatedList<VMTblVariant>.CreateAsync(dataView, pg.pageNumber ?? 1, pageSize) );
        }
    }
}
