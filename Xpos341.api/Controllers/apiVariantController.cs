using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiVariantController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();

        private int idUser = 1;

        public apiVariantController(Xpos341Context _db)
        {
            db = _db;   
        }

        [HttpGet("GetAllData")]
        public List<VMTblVariant> GetAllData()
        {
            List<VMTblVariant> data = (from v in db.TblVariants
                                       join c in db.TblCategories on v.IdCategory equals c.Id
                                       where v.IsDelete == false
                                       select new VMTblVariant
                                       {
                                           Id = v.Id,
                                           NameVariant = v.NameVariant,
                                           Description = v.Description,

                                           IdCategory = v.IdCategory,
                                           NameCategory = c.NameCategory,
                                           
                                           IsDelete = v.IsDelete,
                                           CreateDate = v.CreateDate
                                       }).ToList();

            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMTblVariant GetById(int id)
        {
            VMTblVariant data = (from v in db.TblVariants
                                 join c in db.TblCategories on v.IdCategory equals c.Id
                                 where v.IsDelete == false && v.Id == id
                                 select new VMTblVariant
                                 {
                                     Id = v.Id,
                                     NameVariant = v.NameVariant,
                                     Description = v.Description,

                                     IdCategory = v.IdCategory,
                                     NameCategory = c.NameCategory,

                                     IsDelete = v.IsDelete,
                                     CreateDate = v.CreateDate
                                 }).FirstOrDefault()!;

            return data;
        }

        [HttpGet("CheckByName/{name}/{id}/{idCategory}")]
        public bool CheckName(string name, int id, int idCategory)
        {
            TblVariant data = new TblVariant();

            if (id == 0)
            {
                data = db.TblVariants.Where(a => a.NameVariant == name 
                                            && a.IsDelete == false
                                            && a.IdCategory == idCategory).FirstOrDefault()!;
            }
            else
            {
                data = db.TblVariants.Where(a => a.NameVariant == name
                                            && a.IsDelete == false
                                            && a.IdCategory == idCategory
                                            && a.Id != id).FirstOrDefault()!;
            }

            if(data != null)
                return true;
            
            return false;
        }

        [HttpGet("GetDataByIdCategory")]
        public List<VMTblVariant> GetDataByIdCategory(int idCategory)
        {
            List<VMTblVariant> data = (from v in db.TblVariants
                                       join c in db.TblCategories on v.IdCategory equals c.Id
                                       where v.IsDelete == false && v.IdCategory == idCategory
                                       select new VMTblVariant
                                       {
                                           Id = v.Id,
                                           NameVariant = v.NameVariant,
                                           Description = v.Description,

                                           IdCategory = v.IdCategory,
                                           NameCategory = c.NameCategory,

                                           IsDelete = v.IsDelete,
                                           CreateDate = v.CreateDate
                                       }).ToList();

            return data;
        }
        [HttpPost("Save")]
        public VMResponse Save(TblVariant data)
        {
            data.CreateDate = DateTime.Now;
            data.IsDelete = false;
            data.Description = data.Description?? "Desc of " + data.NameVariant;

            try
            {
                db.Add(data);
                db.SaveChanges();

                response.Message = "Data added successfully";
                response.Entity = data;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Save failed : " + ex.Message;
            }
            return response;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(TblVariant data)
        {
            TblVariant dt = db.TblVariants.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.NameVariant = data.NameVariant;
                dt.Description = data.Description;
                dt.IdCategory = data.IdCategory;
                dt.UpdateBy = idUser;
                dt.UpdateDate = DateTime.Now;

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
            TblVariant dt = db.TblVariants.Where(a => a.Id == id).FirstOrDefault();

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

        [HttpPut("MultipleDelete")]
        public VMResponse MultipleDelete(List<int> ids)
        {

            if (ids.Count > 0)
            {
                foreach (int id in ids)
                {
                    TblVariant dt = db.TblVariants.Where(a => a.Id == id).FirstOrDefault();
                    dt.IsDelete = true;
                    dt.UpdateBy = idUser;
                    dt.UpdateDate = DateTime.Now;

                    db.Update(dt);
                }
                try
                {
                    db.SaveChanges();
                    response.Message = "Data deleted successfully";
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Fail to delete : " + ex.Message;
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
