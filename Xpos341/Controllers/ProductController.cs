using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using xpos341.datamodels;
using xpos341.viewmodels;
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

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter,
                                         int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 5;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSort = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<VMTblProduct> data = await productService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameCategory.ToLower().Contains(searchString.ToLower())
                                  || a.NameVariant.ToLower().Contains(searchString.ToLower())
                                  || a.NameProduct.ToLower().Contains(searchString.ToLower()))
                           .ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.NameProduct).ToList(); break;
                case "price_desc":
                    data = data.OrderByDescending(a => a.Price).ToList(); break;
                case "price":
                    data = data.OrderBy(a => a.Price).ToList(); break;
                default:
                    data = data.OrderBy(a => a.NameProduct).ToList(); break;
            }

            return View(PaginatedList<VMTblProduct>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 5));
        }

        public async Task<JsonResult> CheckNameExist(string nameProduct, int id, int idVariant)
        {
            bool isExists = await productService.CheckVariantByName(nameProduct, id, idVariant);
            return Json(isExists);
        }

        public async Task<IActionResult> Create()
        {
            List<VMTblVariant> variants = await variantService.GetAllData();
            ViewBag.DropDownVariant = variants;

            List<TblCategory> category = await categoryService.GetAllData();
            ViewBag.DropDownCategory = category;

            TblProduct data = new TblProduct();

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblProduct dataParam)
        {
            dataParam.CreateDate = DateTime.Now;
            dataParam.CreateBy = idUser;

            VMResponse response = await productService.Create(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            List<VMTblVariant> variants = await variantService.GetAllData();
            ViewBag.DropDownVariant = variants;

            List<TblCategory> category = await categoryService.GetAllData();
            ViewBag.DropDownCategory = category;

            VMTblProduct data = await productService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblProduct dataParam)
        {
            dataParam.UpdateBy = idUser;
            VMResponse response = await productService.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return PartialView(dataParam);
        }
        public async Task<IActionResult> Detail(int id)
        {
            VMTblProduct data = await productService.GetDataById(id);
            return PartialView(data);
        }
        public async Task<IActionResult> Delete(int id)
        {
            VMTblProduct data = await productService.GetDataById(id);
            return PartialView(data);
        }
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = idUser;
            VMResponse response = await productService.Delete(id);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");
        }
    }
}
