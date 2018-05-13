using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string BatchNumber { get; set; }
        public decimal Cost { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal MarkUp { get; set; }
        public int StockQuantity { get; set; }
        public string ExpirationDate { get; set; }
        public int UnitSizeId { get; set; }
        public string Supplier { get; set; }
        public string PurchaseDate { get; set; }

        public IEnumerable<SelectListItem> UnitSizeList { get; set; }
    }
}