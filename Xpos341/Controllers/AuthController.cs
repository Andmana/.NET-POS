using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class AuthController : Controller
    {
        private AuthService authService;
        private RoleService roleService;
        //private roleService roleService;
        private VMResponse response = new VMResponse();


        public AuthController(AuthService _authService, RoleService _roleService)
        {
            authService = _authService;
            this.roleService = roleService;
            roleService = _roleService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> LoginSubmit(string email, string password)
        {
            VMTblCustomer customer = await authService.CheckLogin(email, password);

            if (customer != null)
            {
                response.Message = $"Hello, {customer.NameCustomer} Welcome to Xpos";
                HttpContext.Session.SetInt32("IdCustomer", customer.Id);
                HttpContext.Session.SetString("NameCustomer", customer.NameCustomer);
                HttpContext.Session.SetInt32("IdRole", customer.IdRole ?? 0);

            }
            else
            {
                response.Success = false;
                response.Message = $"Upps, {email} not found or password is wrong";
            }

            return Json(new {dataResponse = response});
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register()
        {
            VMTblCustomer data = new VMTblCustomer();

            List<VMTblRole> listRole = await roleService.GetAllData();
            ViewBag.listRole = listRole;

            return PartialView(data);
        }
    }
}
