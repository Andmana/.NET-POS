using Microsoft.AspNetCore.Mvc;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class RoleController : Controller
    {
        private RoleService roleService;
        private VMResponse vmResponse = new VMResponse();

        public RoleController(RoleService _roleService)
        {
            roleService = _roleService;
        }
        public async Task< IActionResult > Index()
        {
            List<VMTblRole> roles = await roleService.GetAllData();
            return View(roles);
        }


    }
}
