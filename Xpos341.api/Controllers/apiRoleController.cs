using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiRoleController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int idUser = 1;

        public apiRoleController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMTblRole> GetAllData()
        {
            List<VMTblRole> data = (from r in db.TblRoles 
                                  where r.IsDelete == false
                                  select new VMTblRole
                                  {
                                      Id = r.Id,
                                      RoleName = r.RoleName,
                                      IsDelete = r.IsDelete,
                                      CreatedBy = r.CreatedBy,
                                      CreatedDate = r.CreatedDate,
                                  }).ToList();

            return data;
        }

        [HttpGet("GetById/{id}")]
        public VMTblRole GetById(int id)
        {
            VMTblRole role = (from r in db.TblRoles
                              where r.IsDelete == false && r.Id == id
                              select new VMTblRole
                              {
                                  Id = r.Id,
                                  RoleName = r.RoleName,
                                  IsDelete = r.IsDelete,
                                  CreatedBy = r.CreatedBy,
                                  CreatedDate = r.CreatedDate,
                              }).FirstOrDefault()!;

            return role;
        }

        [HttpGet("CheckByName/{name}/{id}/")]
        public bool CheckName(string name, int id)
        {
            TblRole data = new TblRole();

            if (id == 0)
            {
                data = db.TblRoles.Where(a => a.RoleName == name
                                            && a.IsDelete == false).FirstOrDefault()!;
            }
            else
            {
                data = db.TblRoles.Where(a => a.RoleName == name
                                            && a.IsDelete == false
                                            && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)
                return true;

            return false;
        }

        [HttpPost("Add")]
        public VMResponse Add(TblRole data)
        {
            data.CreatedDate = DateTime.Now;
            data.CreatedBy = idUser;
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
        public VMResponse Edit(TblRole data)
        {
            TblRole dt = db.TblRoles.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.RoleName = data.RoleName;
                dt.UpdatedBy = idUser;
                dt.UpdatedDate = DateTime.Now;

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
            TblRole dt = db.TblRoles.Where(a => a.Id == id).FirstOrDefault();

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.UpdatedBy = idUser;
                dt.UpdatedDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Role deleted successfully";
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
