using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMTblMenu
    {
        [Key]
        public int Id { get; set; }

        public string MenuName { get; set; } = null!;

        public string MenuAction { get; set; } = null!;

        public string MenuController { get; set; } = null!;

        public string? MenuIcon { get; set; }

        public int? MenuSorting { get; set; }

        public bool? IsParent { get; set; }

        public int? MenuParent { get; set; }

        public bool? IsDelete { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
