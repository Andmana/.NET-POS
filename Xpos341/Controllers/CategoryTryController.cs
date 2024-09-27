using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class CategoryTryController : Controller
    {
        private readonly Xpos341Context db ;
        private readonly CategoryTryService categoryTryService;

        public CategoryTryController(Xpos341Context _db)
        {
            db = _db;
            this.categoryTryService = new CategoryTryService(db);
        }
        // GET: CategoryTry
        public IActionResult Index()
        {
            List<VMTblCategory> dataView = categoryTryService.GetAllData();
            return View(dataView);
        }

        public IActionResult Create()
        {
            VMTblCategory dataView = new VMTblCategory();
            return PartialView(dataView);
        }

        [HttpPost]
        public IActionResult Create(VMTblCategory dataView)
        {
            VMResponse response = new VMResponse();

            if (ModelState.IsValid)
            {
                response = categoryTryService.Create(dataView);

                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
            }

            response.Entity = dataView;
            return View(response.Entity);
        }

        public IActionResult Edit(int id)
        {
            VMTblCategory dataView = categoryTryService.GetById(id);
            return PartialView(dataView);
        }
                
        [HttpPost]
        public IActionResult Edit(VMTblCategory dataView)
        {
            VMResponse response = new VMResponse();

            if (ModelState.IsValid)
            {
                response = categoryTryService.Edit(dataView);

                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            response.Entity = dataView;
            return View(response.Entity);
        }

        public IActionResult Detail(int id)
        {
            VMTblCategory dataView = categoryTryService.GetById(id);
            return PartialView(dataView);
        }


        public IActionResult Delete(int id)
        {
            VMTblCategory dataView = categoryTryService.GetById(id);
            return PartialView(dataView);
        }

        [HttpPost]
        public IActionResult Delete(VMTblCategory dataView)
        {
            VMResponse response = new VMResponse();
            response = categoryTryService.Delete(dataView);
            if (response.Success)
            {
                return RedirectToAction("Index");
            }
            return View(response.Entity);
        }


    }
}
