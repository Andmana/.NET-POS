using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class RoleController : Controller
    {
        private RoleService roleService;
        private VMResponse vmResponse = new VMResponse();
        private int idUser = 1;

        public RoleController(RoleService _roleService)
        {
            roleService = _roleService;
        }
        public async Task< IActionResult > Index(string sortOrder, string searchString, string currentFilter,
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

            List<VMTblRole> data = await roleService.GetAllData();


            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.RoleName.ToLower().Contains(searchString.ToLower()))
                           .ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    data = data.OrderByDescending(a => a.RoleName).ToList(); break;
                default:
                    data = data.OrderBy(a => a.RoleName).ToList(); break;
            }
            return View(PaginatedList<VMTblRole>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 5));
        }

        public async Task<JsonResult> CheckNameExist(string name, int id)
        {
            bool isExists = await roleService.CheckByName(name, id);
            return Json(isExists);
        }

        public async Task<IActionResult> Create()
        {
            VMTblRole data = new VMTblRole();

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMTblRole dataParam)
        {
            dataParam.CreatedBy = idUser;
            dataParam.CreatedDate = DateTime.Now;

            VMResponse response = await roleService.Add(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            VMTblRole data = await roleService.GetDataById(id);

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMTblRole dataParam)
        {
            dataParam.CreatedBy = idUser;
            dataParam.CreatedDate = DateTime.Now;

            VMResponse response = await roleService.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Detail(int id)
        {
            VMTblRole data = await roleService.GetDataById(id);

            return PartialView(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMTblRole data = await roleService.GetDataById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = idUser;
            VMResponse response = await roleService.Delete(id);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");
        }

        //UNTUK ATUR MENU ACCESS
        public async Task<IActionResult> Index_MenuAccess(string sortOrder, string searchString,
                                    string currentFilter, int? pageNumber, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentPageSize = pageSize;
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "role_name" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            // Get data from API
            List<VMTblRole> data = await roleService.GetAllData();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(a => a.RoleName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            switch (sortOrder)
            {
                case "role_name":
                    data = data.OrderByDescending(a => a.RoleName).ToList();
                    break;
                default:
                    data = data.OrderBy(a => a.RoleName).ToList();
                    break;
            }

            return View(PaginatedList<VMTblRole>.CreateAsync(data, pageNumber ?? 1, pageSize ?? 3));
        }

        public async Task<IActionResult> Edit_MenuAccess(int id)
        {
            VMTblRole data = await roleService.GetDataById_MenuAccess(id);
            ViewBag.role_menu = data.role_menu;
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit_MenuAccess(VMTblRole dataParam)
        {
            dataParam.UpdatedBy = idUser;

            VMResponse response = await roleService.Edit_MenuAccess(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return View(dataParam);
        }
    }
}
