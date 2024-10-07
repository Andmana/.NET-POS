using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiOrderController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int idUser = 1;

        public apiOrderController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("GetAllDataOrderHeader")]
        public List<TblOrderHeader> GetAllDataOrderHeader()
        {
            List<TblOrderHeader> data = db.TblOrderHeaders.Where(a => a.IsDelete ==  false).ToList();
            return data;
        }

        [HttpGet("GetDataOrderDetail/{id}")]
        public List<VMOrderDetail> GetDataOrderDetail(int id)
        {
            List<VMOrderDetail> data = (from d in db.TblOrderDetails
                                     join p in db.TblProducts on d.IdProduct equals p.Id
                                     where d.IsDelete == false && d.IdHeader == id
                                     select new VMOrderDetail
                                     {
                                         Id = d.Id,
                                         Qty = d.Qty,
                                         SumPrice = d.SumPrice,

                                         IdProduct = d.IdProduct,
                                         NameProduct = p.NameProduct,
                                         Price = p.Price,
                                         Stock = p.Stock,

                                         CreateBy = d.CreateBy,
                                         CreateDate = d.CreateDate,
                                         UpdateBy = d.UpdateBy,
                                         UpdateDate = d.UpdateDate,

                                     }).ToList();

            return data;
        }

        [HttpPost("SubmitOrder")]
        public VMResponse SubmitOrder(VMOrderHeader dataHeader)
        {
            TblOrderHeader head = new TblOrderHeader();

            head.Amount = dataHeader.Amount;
            head.TotalQty = dataHeader.TotalQty;
            head.IdCostumer = idUser;
            head.IsCheckout = false;
            head.CreateBy = idUser;
            head.CreateDate = DateTime.Now;

            try {
                db.Add(head);
                db.SaveChanges();

                foreach (var item in dataHeader.ListDetails)
                {
                    TblOrderDetail detail = new TblOrderDetail();

                    detail.IdHeader = head.Id;
                    detail.IdProduct = item.IdProduct;
                    detail.Qty = item.Qty;
                    detail.IsDelete = false;
                    detail.SumPrice = item.SumPrice;
                    detail.CreateDate = DateTime.Now;
                    detail.CreateBy = idUser;

                    try
                    {
                        db.Add(detail);
                        db.SaveChanges();

                        TblProduct dataProduct = db.TblProducts.Where(a => a.Id == item.IdProduct).FirstOrDefault();
                        if (dataProduct != null)
                        {
                            dataProduct.Stock = dataProduct.Stock - item.Qty;

                            db.Update(dataProduct);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = "Save Detail failed : " + ex.Message;
                    }
                }

            }
            catch(Exception ex) {
                response.Success = false;
                response.Message = "Save failed : " + ex.Message;
            }

            return response;
        }
    }
}
