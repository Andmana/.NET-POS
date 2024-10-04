using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Xpos341.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class apiProductController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();

        private int idUser = 1;

        public apiProductController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("GetALlData")]
        public List<VMTblProduct> GettAllData()
        {
            List<VMTblProduct> data = (from p in db.TblProducts
                                       join v in db.TblVariants on p.IdVariant equals v.Id
                                       join c in db.TblCategories on v.IdCategory equals c.Id into tc
                                       from tcategory in tc.DefaultIfEmpty()
                                       where p.IsDelete == false && v.IsDelete == false
                                       select new VMTblProduct
                                       {
                                           Id = p.Id,
                                           NameProduct = p.NameProduct,
                                           Price = p.Price,
                                           Stock = p.Stock,
                                           Image = p.Image,

                                           IdVariant = v.Id,
                                           NameVariant = v.NameVariant,

                                           IdCategory = v.IdCategory,
                                           NameCategory = tcategory.NameCategory ?? "[Blank]",

                                           CreateDate = p.CreateDate
                                       }).ToList();

            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMTblProduct GetById(int id)
        {
            VMTblProduct data = (from p in db.TblProducts
                                 join v in db.TblVariants on p.IdVariant equals v.Id
                                 join c in db.TblCategories on v.IdCategory equals c.Id into tc
                                 from tcategory in tc.DefaultIfEmpty()
                                 where p.IsDelete == false && v.IsDelete == false
                                    && p.Id == id
                                 select new VMTblProduct
                                 {
                                     Id = p.Id,
                                     NameProduct = p.NameProduct,
                                     Price = p.Price,
                                     Stock = p.Stock,
                                     Image = p.Image,

                                     IdVariant = v.Id,
                                     NameVariant = v.NameVariant,

                                     IdCategory = v.IdCategory,
                                     NameCategory = tcategory.NameCategory ?? "[Blank]",

                                     CreateDate = p.CreateDate
                                 }).FirstOrDefault()!;

            return data;
        }

        [HttpGet("CheckByName/{name}/{id}/{idCategory}")]
        public bool CheckName(string name, int id, int idVariant)
        {
            TblProduct data = new TblProduct();

            if (id == 0)
            {
                data = db.TblProducts.Where(a => a.NameProduct == name
                                            && a.IsDelete == false
                                            && a.IdVariant == idVariant).FirstOrDefault()!;
            }
            else
            {
                data = db.TblProducts.Where(a => a.NameProduct == name
                                            && a.IsDelete == false
                                            && a.IdVariant == idVariant
                                            && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)
                return true;

            return false;
        }
        [HttpPost("Save")]
        public VMResponse Save(TblProduct data)
        {
            data.CreateDate = DateTime.Now;
            data.CreateBy = idUser;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();
                response.Message = "Data added successfuy";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to add : " + ex.Message;
            }

            return response;
        }


        [HttpPut("Edit")]
        public VMResponse Edit(TblProduct data)
        {
            TblProduct dt = db.TblProducts.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.NameProduct = data.NameProduct;
                dt.IdVariant = data.IdVariant;
                dt.Price = data.Price;
                dt.Stock = data.Stock;
                dt.UpdateBy = idUser;
                dt.UpdateDate = DateTime.Now;
                
                if(data.Image != null)
                    dt.Image = data.Image;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Data updated successfully";
                    response.Entity = dt;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Update failed : " + ex.Message;
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Data not found";
            }

            return response;
        }

        [HttpDelete("Delete/{id}")]
        public VMResponse Delete(int id)
        {
            TblProduct dt = db.TblProducts.Where(a => a.Id == id).FirstOrDefault();

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.UpdateBy = idUser;
                dt.UpdateDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Data deleted successfully";
                    response.Entity = dt;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Delete failed : " + ex.Message;
                }

            }
            else
            {
                response.Success = false;
                response.Message = "Data not found";
            }

            return response;
        }



    }
}
