using Microsoft.AspNetCore.Mvc;
using System.Data;
using xpos341.datamodels;
using xpos341.viewmodels;
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


        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, 
                                         int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            pageNumber = pageNumber ?? 1;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null) 
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<TblCategory> data = await categoryService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameCategory.ToLower().Contains(searchString.ToLower())
                                  ||   a.Description.ToLower().Contains(searchString.ToLower()) )
                           .ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.NameCategory).ToList(); break;
                default:
                    data = data.OrderBy(a => a.NameCategory).ToList(); break;
            }

            return View(PaginatedList<TblCategory>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public IActionResult Create()
        {
            TblCategory data = new TblCategory();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblCategory dataParam)
        {
            dataParam.CreateBy = idUser;
            dataParam.CreateDate = DateTime.Now;
            VMResponse response = await categoryService.Create(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return PartialView(dataParam);
        }

        public async Task<JsonResult> CheckNameExist(string nameCategory, int id)
        {
            bool isExists = await categoryService.CheckCategoryByName(nameCategory, id);
            return Json(isExists);
        }

        public async Task<IActionResult> Edit(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TblCategory dataParam)
        {
            dataParam.UpdateBy = idUser;
            VMResponse response = await categoryService.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return PartialView(dataParam);
        }

        public async Task<IActionResult> Detail(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return View(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TblCategory data = await categoryService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = idUser;
            VMResponse response = await categoryService.Delete(id, createBy);

            if (response.Success)
            {
                return Json(new {dataResponse = response});
            }

            return RedirectToAction("Index");
        }
    }
}
