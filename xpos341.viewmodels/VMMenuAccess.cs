using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMMenuAccess
    {
        public int Id { get; set; }

        public string? MenuName { get; set; }
        public string? MenuController{ get; set; }
        public string? MenuAction { get; set; }
        public string? MenuIcon { get; set; }
        public int? MenuSorting { get; set; }
        public bool? IsParent { get; set; }

        public int? MenuParent {  get; set; }
        public int? IdRole {get; set; }
        public string? NameRole { get; set; }
        public int? IdMenu {  get; set; }
        public bool is_selected { get; set; }
        public List<VMMenuAccess>? List_Child { get; set; }
    }
}
