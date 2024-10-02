using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiCategoryController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int idUser = 1;

        public apiCategoryController(Xpos341Context db)
        {
            this.db = db;
        }

        [HttpGet("GetAllData")]
        public List<TblCategory> GetAllData()
        {
            List<TblCategory> data = db.TblCategories.Where(a => a.IsDelete == false).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public TblCategory GetById(int id)
        {
            TblCategory category = db.TblCategories.Where(a => a.Id == id).FirstOrDefault();
            if (category == null) { 
            }
            return category;
        }

        [HttpGet("GetCategoryByname/{name}/{id}")]
        public bool CheckName(string name, int id)
        {
            TblCategory data = new TblCategory();

            //untuk create
            if (id == 0)
            {
                data = db.TblCategories.Where(a => a.NameCategory == name && a.IsDelete == false).FirstOrDefault();
            }else //untuk edit
            {
                data = db.TblCategories.Where(a => a.NameCategory == name && a.IsDelete == false && a.Id != id).FirstOrDefault();
            }

            if (data != null)
            {
                return true;
            }
            return false;
        }

        [HttpPost("Save")]
        public VMResponse Save(TblCategory data)
        {
            data.CreateBy = idUser;
            data.CreateDate = DateTime.Now;
            data.IsDelete = false;
            data.Description = data.Description ?? "Desc of " + data.NameCategory;

            try
            {
                db.Add(data);
                db.SaveChanges();
                response.Message = "Data Added Succesfuly";
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = "Failed to save : " + ex.Message;

            }
            response.Entity = data;

            return response;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(TblCategory data)
        {
            TblCategory dt = db.TblCategories.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.NameCategory = data.NameCategory;
                dt.Description = data.Description;
                dt.UpdateBy = idUser;
                dt.UpdateDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Data Edited Succesfuly";
                }
                catch (Exception ex) 
                {
                    response.Success=false;
                    response.Message = "Data Fail to Updated : " + ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Data Not Found";
            }
            response.Entity = data;

            return response;
        }

        [HttpDelete("Delete/{id}/{createBy}")]
        public VMResponse Delete(int id, int createBy)
        {

            TblCategory dt = db.TblCategories.Where(a => a.Id == id).FirstOrDefault();

            if (dt != null)
            {
                dt.IsDelete = false;
                dt.UpdateBy = createBy;
                dt.UpdateDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Data Delete Succesfuly";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Data Fail to Delete : " + ex.Message;
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Data Not Found";
            }

            return response;

        }


    }
}
