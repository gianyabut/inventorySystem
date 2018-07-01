using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class OrderDetailViewModel
    {
        public int SaleDetailsId { get; set; }
        public int SaleId { get; set; }

        public int StockId { get; set; }
        public int BrandId { get; set; }
        public int UnitSizeId { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal SRP { get; set; }
        public decimal SRPDC { get; set; }
    }
}