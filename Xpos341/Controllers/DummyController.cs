using Microsoft.AspNetCore.Mvc;

namespace Xpos341.Controllers
{
    public class DummyController : Controller
    {
        // To access from view -> asp-controller = (controller name) asp-action=(FunctionName)
        // ex => "asp-controller=Dummy"  asp-action=Bootcamp
        // To create view page/file -> right click the "View()" -> Add View..
        public IActionResult Bootcamp()
        {
            return View();
        }
    }
}
