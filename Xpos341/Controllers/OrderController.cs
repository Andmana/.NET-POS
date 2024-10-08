using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class OrderController : Controller
    {
        private ProductService productService;
        private OrderService orderService;
        private int idUser = 1;

        public OrderController(ProductService _productService, OrderService _orderService)
        {
            productService = _productService;
            orderService = _orderService;
        }

        public async Task<IActionResult> Catalog(VMSearchPage dataSearch)
        {
            List<VMTblProduct> dataProduct = await productService.GetAllData();

            dataSearch.MinAmount = dataSearch.MinAmount == null ? decimal.MinValue : dataSearch.MinAmount;
            dataSearch.MaxAmount = dataSearch.MaxAmount == null ? decimal.MaxValue : dataSearch.MaxAmount;
            
            if(dataSearch.NameProduct != null)
                dataProduct = dataProduct.Where(a => a.NameProduct.ToLower().Contains(dataSearch.NameProduct.ToLower())).ToList();
            
            if (dataSearch.MinAmount != null && dataSearch.MaxAmount != null)
            {
                dataProduct = dataProduct.Where(a => a.Price >= dataSearch.MinAmount
                                                && a.Price <= dataSearch.MaxAmount).ToList();
            }

            //Get session in VMOrderHeader first load
            VMOrderHeader dataHeader = HttpContext.Session.GetComplexData<VMOrderHeader>("ListCart");
            if (dataHeader == null)
            {
                dataHeader = new VMOrderHeader();
                dataHeader.ListDetails = new List<VMOrderDetail>();
            }

            var listDetail = JsonConvert.SerializeObject(dataHeader.ListDetails);

            ViewBag.dataHeader = dataHeader;
            ViewBag.dataDetail = listDetail;

            ViewBag.Search = dataSearch;
            ViewBag.currentPageSize = dataSearch.pageSize;
            
            
            return View(PaginatedList<VMTblProduct>.CreateAsync(dataProduct, dataSearch.pageNumber ?? 1, dataSearch.pageSize ?? 3));
        }

        // set session
        [HttpPost]
        public JsonResult SetSession(VMOrderDetail dataHeader)
        {
            HttpContext.Session.SetComplexData("ListChart", dataHeader);
            return Json("");
        }

        //remove session
        public JsonResult Remove()
        {
            HttpContext.Session.Remove("ListCart");
            //HttpContext.Session.Clear(); // Delete all session

            return Json("");
        }
        public IActionResult OrderPreview(VMOrderHeader dataHeader)
        {
            return PartialView(dataHeader);
        }

        public async Task<IActionResult> SearchMenu()
        {
            return PartialView();
        }
    }
}
