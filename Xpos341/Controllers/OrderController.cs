﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using xpos341.viewmodels;
using Xpos341.Services;

namespace Xpos341.Controllers
{
    public class OrderController : Controller
    {
        private ProductService productService;
        private OrderService orderService;
        private int IdUser = 1;

        public OrderController(ProductService productService, OrderService orderService)
        {
            this.productService = productService;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Catalog(VMSearchPage dataSearch)
        {
            List<VMTblProduct> dataProduct = await productService.GetAllData();

            dataSearch.MinAmount = dataSearch.MinAmount == null ? decimal.MinValue : dataSearch.MinAmount;
            dataSearch.MaxAmount = dataSearch.MaxAmount == null ? decimal.MaxValue : dataSearch.MaxAmount;
            //dataSearch.MaxAmount = dataSearch.MaxAmount ?? decimal.MaxValue; //sama saja (ternary)

            if (dataSearch.NameProduct != null)
            {
                dataProduct = dataProduct.Where(a => a.NameProduct.ToLower().Contains(dataSearch.NameProduct!.ToLower())).ToList();
            }

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

            var ListDetail = JsonConvert.SerializeObject(dataHeader.ListDetails);
            ViewBag.dataHeader = dataHeader;
            ViewBag.dataDetail = ListDetail;

            ViewBag.Search = dataSearch;
            ViewBag.CurrentPageSize = dataSearch.pageSize;


            return View(PaginatedList<VMTblProduct>.CreateAsync(dataProduct, dataSearch.pageNumber ?? 1,
                        dataSearch.pageSize ?? 3));
        }

        // Set Session in VMOrderHeader every add or minus
        [HttpPost]
        public JsonResult SetSession(VMOrderHeader dataHeader)
        {
            HttpContext.Session.SetComplexData("ListCart", dataHeader);
            return Json("");
        }

        // Remove Session
        [HttpPost]
        public JsonResult RemoveSession()
        {
            HttpContext.Session.Remove("ListCart");
            //HttpContext.Session.Clear(); // clear all session
            return Json("");
        }

        public IActionResult OrderPreview(VMOrderHeader dataHeader)
        {
            return PartialView(dataHeader);
        }

        public IActionResult SearchMenu()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> SubmitOrder(VMOrderHeader dataHeader)
        {
            //dataHeader.IdCostumer = httpcon
            dataHeader.IdCostumer = IdUser;

            VMResponse response = await orderService.SubmitOrder(dataHeader);
            return Json(response);
        }

        public async Task<IActionResult> HistoryOrder(VMSearchPage dataSearch)
        {
            //dataHeader.IdCostumer = httpcon
            int IdCostumer = IdUser;
            List<VMOrderHeader> data= await orderService.GetDataOrderHeaderDetail(IdCostumer);
            int count = await orderService.CountTransaction(IdCostumer);
            ViewBag.count = count;


            dataSearch.MinAmount = dataSearch.MinAmount == null ? decimal.MinValue : dataSearch.MinAmount;
            dataSearch.MaxAmount = dataSearch.MaxAmount == null ? decimal.MaxValue : dataSearch.MaxAmount;

            dataSearch.MinDate = dataSearch.MinDate == null ? DateTime.MinValue : dataSearch.MinDate;
            dataSearch.MaxDate = dataSearch.MaxDate == null ? DateTime.MaxValue : dataSearch.MaxDate;

            if (!string.IsNullOrEmpty(dataSearch.CodeTransaction))
            {
                data = data.Where(a => a.CodeTransaction.ToLower()
                                        .Contains(dataSearch.CodeTransaction.ToLower())).ToList();
            }

            if (dataSearch.MinDate != null && dataSearch.MaxDate != null)
            {
                data = data.Where(a => a.CreateDate.Date >= dataSearch.MinDate
                                    && a.CreateDate <= dataSearch.MaxDate).ToList();
            }

            if (dataSearch.MinAmount != null && dataSearch.MaxAmount != null)
            {
                data = data.Where(a => a.Amount >= dataSearch.MinAmount
                                    && a.Amount <= dataSearch.MaxAmount).ToList();
            }

            ViewBag.search = dataSearch;

            return View(data);
        }

        public IActionResult Search(VMSearchPage dataSearch)
        {
            return PartialView(dataSearch);
        }


    }
}
