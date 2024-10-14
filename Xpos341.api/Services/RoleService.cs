using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.api.Services
{
    public class RoleService
    {
        private readonly Xpos341Context db;
        private VMResponse response = new VMResponse();

        public RoleService(Xpos341Context _db)
        {
            db = _db;
        }

        public async Task<List<VMMenuAccess>> GetMenuAccessParentChildByRoleId(int idRole, int MenuParent, bool isSelected = false)
        {
            List<VMMenuAccess> result =  new List<VMMenuAccess> ();
            List<TblMenu> data = db.TblMenus.Where(a => a.MenuParent == MenuParent && a.IsDelete == false)
                                            .ToList ();

            foreach (TblMenu item in data)
            {
                VMMenuAccess list = new VMMenuAccess();

                list.IdMenu = item.Id;
                list.MenuName = item.MenuName;
                list.IsParent = item.IsParent;
                list.MenuParent = item.MenuParent;
                list.is_selected = db.TblMenuAccesses.Where(a => a.RoleId == idRole && a.MenuId == item.Id && a.IsDelete == false).Any();
                list.List_Child = await GetMenuAccessParentChildByRoleId(idRole, item.Id, isSelected);
                
                if (isSelected)
                {
                    if (list.is_selected) 
                        result.Add(list);
                }
                else
                    result.Add(list);
            }
            
            return result;
            
        }
    }
}
