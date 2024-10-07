using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xpos341.viewmodels
{
    public class VMOrderHeader
    {
        public int Id { get; set; }

        public string CodeTransaction { get; set; } = null!;

        public int IdCostumer { get; set; }

        public decimal Amount { get; set; }

        public int TotalQty { get; set; }

        public bool IsCheckout { get; set; }

        public bool? IsDelete { get; set; }

        public int CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public List<VMOrderDetail> ListDetails { get; set; }
    }
}
