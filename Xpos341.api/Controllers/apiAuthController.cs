using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiAuthController : ControllerBase
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();
        private int idUser = 1;

        public apiAuthController(Xpos341Context _db)
        {
            db = _db;
        }

        [HttpGet("CheckLogin/{email}/{password}")]
        public VMTblCustomer CheckLogin(string email, string password)
        {
            VMTblCustomer data = (from c in db.TblCustomers
                                  join r in db.TblRoles on c.IdRole equals r.Id
                                  where c.IsDelete == false && c.Email == email && c.Password == password
                                  select new VMTblCustomer
                                  {
                                      Id = c.Id,
                                      NameCustomer = c.NameCustomer,
                                      Email = email,
                                      IdRole = c.IdRole,
                                      RoleName = r.RoleName,

                                  }).FirstOrDefault()!;

            return data;
        }

        [HttpGet("MenuAccess/{IdRole}")]
        public List<VMMenuAccess> MenuAccess(int IdRole)
        {
            List<VMMenuAccess> listMenu = (from parent in db.TblMenus
                                           join ma in db.TblMenuAccesses on parent.Id equals ma.MenuId
                                           where parent.IsParent == true && ma.RoleId == IdRole
                                           && parent.IsDelete == false && ma.IsDelete == false
                                           select new VMMenuAccess
                                           {
                                               Id = parent.Id,
                                               MenuName = parent.MenuName,
                                               MenuAction = parent.MenuAction,
                                               MenuController = parent.MenuController,
                                               MenuIcon = parent.MenuIcon,
                                               IdRole = ma.RoleId,
                                               MenuSorting = parent.MenuSorting,
                                               List_Child = (from child in db.TblMenus
                                                            join ma2 in db.TblMenuAccesses on child.Id equals ma2.MenuId
                                                            where child.MenuParent == parent.Id && child.IsDelete == false
                                                            && ma2.IsDelete == false && ma2.RoleId == IdRole
                                                            select new VMMenuAccess
                                                            {
                                                                Id = child.Id,
                                                                MenuName = child.MenuName,
                                                                MenuAction = child.MenuAction,
                                                                MenuController = child.MenuController,
                                                                MenuIcon = child.MenuIcon,
                                                                IdRole = IdRole,
                                                                MenuSorting = child.MenuSorting,
                                                                MenuParent = child.MenuParent,

                                                            }).OrderBy(a => a.MenuSorting).ToList()

                                           }).OrderBy(a => a.MenuSorting).ToList();

            return listMenu;
        }
    }
}
