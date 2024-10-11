using Microsoft.AspNetCore.Mvc;
using Xpos341.Services;
using xpos341.viewmodels;
using xpos341.datamodels;

namespace Xpos341.Controllers
{
    public class CustomerController : Controller
    {
        private static VMPage page = new VMPage();
        private RoleService roleService;
        private CustomerService customerService;

        private int idUser = 1;

        public CustomerController(RoleService roleService,
                                  CustomerService customerService)
        {
            this.roleService = roleService;
            this.customerService = customerService;
            
        }
        public async Task<IActionResult> Index(VMPage pg)

        {
            ViewBag.nameSort = string.IsNullOrEmpty(pg.sortOrder) ? "name_desc" : "";
            ViewBag.rolesort = pg.sortOrder == "name" ? "role_desc" : "role";
            ViewBag.currentSort = pg.sortOrder;
            ViewBag.currentShowData = pg.showData;

            if (pg.searchString != null)
            {
                //pg.pageNumber = pg.;
            }
            else
            {
                pg.currentFilter = pg.searchString;
            }
            ViewBag.currentFilter = pg.searchString;

            List<VMTblCustomer> dataView = await customerService.GetAllData();

            if (!string.IsNullOrEmpty(pg.searchString))
            {
                dataView = dataView.Where(a => a.NameCustomer.ToLower().Contains(pg.searchString.ToLower())
                                            || a.RoleName.ToLower().Contains(pg.searchString.ToLower())
                                            || a.Email.ToLower().Contains(pg.searchString.ToLower())
                                            ).ToList();
                //ViewBag.searchString = pg.searchString;
            }

            switch (pg.sortOrder)
            {
                case "role_desc":
                    dataView = dataView.OrderByDescending(a => a.RoleName).ToList();
                    break;
                case "role":
                    dataView = dataView.OrderBy(a => a.RoleName).ToList();
                    break;
                case "name_desc":
                    dataView = dataView.OrderByDescending(a => a.NameCustomer).ToList();
                    break;
                default:
                    dataView = dataView.OrderBy(a => a.NameCustomer).ToList();
                    break;
            }

            int pageSize = pg.showData ?? 5;
            page = pg;
            //return View(dataView);

            return View(PaginatedList<VMTblCustomer>.CreateAsync(dataView, pg.pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            List<VMTblRole> roles = await roleService.GetAllData();
            ViewBag.DropDownRoles = roles;

            VMTblCustomer data = new VMTblCustomer();

            return PartialView(data);
        }

        public async Task<JsonResult> CheckExists(string email, int id)
        {
            bool isExists = await customerService.CheckExists(email, id);
            return Json(isExists);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMTblCustomer dataParam)
        {
            dataParam.CreateBy = idUser;
            dataParam.CreateDate = DateTime.Now;

            VMResponse response = await customerService.Add(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Edit(int id)
        {
            List<VMTblRole> roles = await roleService.GetAllData();
            ViewBag.DropDownRoles = roles;

            VMTblCustomer data = await customerService.GetById(id);

            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VMTblCustomer dataParam)
        {
            dataParam.CreateBy = idUser;
            dataParam.CreateDate = DateTime.Now;

            VMResponse response = await customerService.Edit(dataParam);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }

            return PartialView(dataParam);
        }

        public async Task<IActionResult> Detail(int id)
        {
            VMTblCustomer data = await customerService.GetById(id);

            return PartialView(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            VMTblCustomer data = await customerService.GetById(id);
            return PartialView(data);
        }

        [HttpPost]
        public async Task<IActionResult> SureDelete(int id)
        {
            int createBy = idUser;
            VMResponse response = await customerService.Delete(id);

            if (response.Success)
            {
                return Json(new { dataResponse = response });
            }
            return RedirectToAction("Index");
        }
    }
}
