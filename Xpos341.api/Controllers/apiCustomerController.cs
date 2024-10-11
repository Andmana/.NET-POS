using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiCustomerController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int idUser = 1;

        public apiCustomerController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMTblCustomer> GetAllData()
        {
            List<VMTblCustomer> data = (from c in db.TblCustomers
                                        join r in db.TblRoles on c.IdRole equals r.Id
                                        where c.IsDelete == false
                                        select new VMTblCustomer
                                        {
                                            Id = c.Id,
                                            NameCustomer = c.NameCustomer,
                                            Email = c.Email,
                                            Address = c.Address,
                                            Phone = c.Phone,
                                            CreateDate = c.CreateDate,

                                            IdRole = c.IdRole,
                                            RoleName = r.RoleName,

                                        }).ToList()!;
            return data;
        }

        [HttpGet("GetById/{id}")]
        public VMTblCustomer GetById(int id)
        {
            VMTblCustomer role = (from c in db.TblCustomers
                                  join r in db.TblRoles on c.IdRole equals r.Id
                                  where c.IsDelete == false && c.Id == id
                                  select new VMTblCustomer
                                  {
                                      Id = c.Id,
                                      NameCustomer = c.NameCustomer,
                                      Email = c.Email,
                                      Address = c.Address,
                                      Phone = c.Phone,
                                      CreateDate = c.CreateDate,

                                      IdRole = c.IdRole,
                                      RoleName = r.RoleName,
                                  }).FirstOrDefault()!;

            return role;
        }

        [HttpGet("CheckExists/{email}/{id}")]
        public bool CheckExists(string email, int id)
        {
            TblCustomer data = new TblCustomer();

            if (id == 0)
            {
                data = db.TblCustomers.Where(a => a.Email == email
                                            && a.IsDelete == false).FirstOrDefault()!;
            }
            else
            {
                data = db.TblCustomers.Where(a => a.Email == email
                                            && a.IsDelete == false
                                            && a.Id != id).FirstOrDefault()!;
            }

            if (data != null)
                return true;

            return false;
        }

        [HttpPost("Add")]
        public VMResponse Add(TblCustomer data)
        {
            data.CreateDate = DateTime.Now;
            data.CreateBy = idUser;
            data.IsDelete = false;
            data.Password = data.Password ?? "qwerty";

            try
            {
                db.Add(data);
                db.SaveChanges();
                response.Message = "Data added successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to add : " + ex.Message;
            }

            return response;
        }


        [HttpPut("Edit")]
        public VMResponse Edit(VMTblCustomer data)
        {
            TblCustomer dt = db.TblCustomers.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.NameCustomer = data.NameCustomer;
                dt.UpdateBy = idUser;
                dt.UpdateDate = DateTime.Now;

                dt.Email = data.Email ?? dt.Email;
                dt.Password = data.Password ?? dt.Password;
                dt.IdRole = data.IdRole ?? dt.IdRole;

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
            TblCustomer dt = db.TblCustomers.Where(a => a.Id == id).FirstOrDefault();

            if (dt != null)
            {
                dt.IsDelete = true;
                dt.UpdateBy = idUser;
                dt.UpdateDate = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    response.Message = "Customer deleted successfully";
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

