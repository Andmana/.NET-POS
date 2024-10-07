using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMOrderDetail
    {
        public int Id { get; set; }

        public string IdHeader { get; set; } = null!;

        public int IdProduct { get; set; }

        public decimal Qty { get; set; }

        public int SumPrice { get; set; }

        public bool? IsDelete { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
