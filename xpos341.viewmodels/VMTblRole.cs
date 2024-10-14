using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMTblRole
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = null!;

        public bool? IsDelete { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<VMMenuAccess> role_menu {  get; set; }
    }
}
