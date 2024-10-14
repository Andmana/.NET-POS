using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;
using Xpos341.api.Services;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiRoleController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private RoleService roleService;
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

        //Untuk atur menu access
        [HttpGet("GetDataById_MenuAccess/{id}")]
        public async Task<VMTblRole> DataById_MenuAccess(int id)
        {
            VMTblRole result = db.TblRoles.Where(a => a.Id == id)
                                          .Select(a => new VMTblRole()
                                          {
                                              Id = a.Id,
                                              RoleName = a.RoleName,
                                          })
                                          .FirstOrDefault()!;

            result.role_menu = await roleService.GetMenuAccessParentChildByRoleId(result.Id, 0, false);
            return result;
        }

        [HttpPut]
        public VMResponse Edit_MenuAccess(VMTblRole data)
        {
            TblRole dt = db.TblRoles.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.RoleName = data.RoleName;
                dt.UpdatedDate = DateTime.Now;
                dt.UpdatedBy = idUser;

                try
                {
                    db.Update(dt);

                    //Save Menu access
                    if (data.role_menu.Count > 0)
                    {
                        // remove menu access
                        List<TblMenuAccess> listMenuAccessRemove = db.TblMenuAccesses.Where(a => a.RoleId == data.Id).ToList();
                        if (listMenuAccessRemove.Count > 0)
                        {
                            foreach (TblMenuAccess item in listMenuAccessRemove)
                            {
                                item.IsDelete = true;
                                item.UpdatedBy = idUser;
                                item.UpdatedDate = DateTime.Now;

                                db.Update(item);
                            }
                        }
                        List<TblMenuAccess> listMenuAccessAdd = data.role_menu.Where(a => a.is_selected == true)
                                                                                  .Select(a => new TblMenuAccess()
                                                                                  {
                                                                                      RoleId = data.Id,
                                                                                      MenuId = a.IdMenu,
                                                                                      IsDelete = false,
                                                                                      CreatedBy = idUser,
                                                                                      CreatedDate = DateTime.Now,

                                                                                  }).ToList();

                        foreach (TblMenuAccess item in listMenuAccessAdd)
                        {
                            db.Add(item);
                        }

                        db.SaveChanges();
                        response.Message = "Data saved";


                    }
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Failed save : " + ex.Message;
                }
            }else
            {
                response.Success = false;
                response.Message = "Data not found";

            }

            return response;
        }
    }
}
