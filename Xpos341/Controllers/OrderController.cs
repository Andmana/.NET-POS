using Microsoft.AspNetCore.Mvc;

namespace Xpos341.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Catalog()
        {
            return View();
        }
    }
}
