using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal SellingPrice { get; set; }

        public int MarkUp { get; set; }
        public int StockQuantity { get; set; }
        public string ExpirationDate { get; set; }
        public int UnitSizeId { get; set; }
        public string UnitSizeName { get; set; }
        public string Supplier { get; set; }
        public string PurchaseDate { get; set; }
        public int ProductGroupId { get; set; }

        public IEnumerable<SelectListItem> UnitSizeList { get; set; }
        public IEnumerable<SelectListItem> ProductGroupList { get; set; }
    }
}