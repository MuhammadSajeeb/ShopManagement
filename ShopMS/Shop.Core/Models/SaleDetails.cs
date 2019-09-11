using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class SaleDetails
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public string Item { get; set; }
        public decimal Qty { get; set; }
        public decimal Unit { get; set; }
        public decimal Total { get; set; }

    }
}
