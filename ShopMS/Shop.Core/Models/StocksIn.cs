using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Models
{
    public class StocksIn
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Mrp { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int CategoriesId { get; set; }
        public string Date { get; set; }

    }
}
