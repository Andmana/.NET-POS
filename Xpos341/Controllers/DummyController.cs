using Microsoft.AspNetCore.Mvc;

namespace Xpos341.Controllers
{
    public class DummyController : Controller
    {
        public IActionResult Bootcamp()
        {
            return View();
        }
    }
}
