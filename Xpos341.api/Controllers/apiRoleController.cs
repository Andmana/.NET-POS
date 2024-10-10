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
                                      CreateBy = r.CreatedBy,
                                      CreateDate = r.CreatedDate,
                                  }).ToList();

            return data;
        }



    }
}
