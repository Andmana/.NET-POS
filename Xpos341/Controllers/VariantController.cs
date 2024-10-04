using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class VariantController : Controller
    {
        private CategoryService categoryService;
        private VariantService variantService;

        private int idUser = 1;

        public VariantController(CategoryService categoryService, VariantService variantService)
        {
            this.categoryService = categoryService;
            this.variantService = variantService;
        }
        public async Task<IActionResult>Index(string sortOrder, string searchString, string currentFilter,
                                         int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 5;
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

            List<VMTblVariant> data = await variantService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.NameVariant.ToLower().Contains(searchString.ToLower())
                                  || a.NameCategory.ToLower().Contains(searchString.ToLower())
                                  || a.Description.ToLower().Contains(searchString.ToLower()))
                           .ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.NameVariant).ToList(); break;
                default:
                    data = data.OrderBy(a => a.NameVariant).ToList(); break;
            }

            return View(PaginatedList<VMTblVariant>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 5));

        }
        public async Task<JsonResult> CheckNameExist(string nameVariant, int id, int idCategory)
        {
            bool isExists = await variantService.CheckVariantByName(nameVariant, id, idCategory);
            return Json(isExists);
        }

        public async Task<IActionResult> Create()
        {
            List<TblCategory> dataCategory = await categoryService.GetAllData();
            ViewBag.DropdownCategory = dataCategory;
            VMTblVariant data = new VMTblVariant();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMTblVariant dataParam)
        {
            dataParam.CreateDate = DateTime.Now;
            dataParam.CreateBy = idUser;

            VMResponse response = await variantService.Create(dataParam);

            if(response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            List<TblCategory> dataCategory = await categoryService.GetAllData();
            ViewBag.DropdownCategory = dataCategory;

            VMTblVariant data = await variantService.GetDataById(id);
            //VMTblVariant data = new VMTblVariant();
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMTblVariant dataParam)
        {
            dataParam.UpdateBy = idUser;
            VMResponse response = await variantService.Edit(dataParam);

            if (response.Success)
            {
                return Json(new {dataResponse = response});
            }
            return PartialView(dataParam);
        }

        public async Task<IActionResult> Detail(int id)
        {
            VMTblVariant data = await variantService.GetDataById(id);
            return PartialView(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMTblVariant data = await variantService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = idUser;
            VMResponse response = await variantService.Delete(id);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MultipleDelete(List<int> ids)
        {
            List<string> listName = new List<string>();

            foreach (int id in ids)
            {
                VMTblVariant data = await variantService.GetDataById(id);
                listName.Add(data.NameVariant);
            }

            ViewBag.listName = listName;

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SureMultipleDelete (List<int> ids)
        {
            VMResponse response = await variantService.MultipleDelete(ids);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");

        }

    }
}

