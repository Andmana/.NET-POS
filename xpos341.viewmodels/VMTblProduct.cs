using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMTblProduct
    {
        public int Id { get; set; }

        public int IdVariant { get; set; }

        public string NameProduct { get; set; } = null!;

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Image { get; set; }

        public bool? IsDelete { get; set; }

        public int CreateBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
    }
}
