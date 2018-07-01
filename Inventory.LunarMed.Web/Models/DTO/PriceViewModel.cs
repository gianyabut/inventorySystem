using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models.DTO
{
    public class PriceViewModel
    {
        public int PriceId { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }

        public int UnitSizeId { get; set; }
        public string UnitSizeName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public IEnumerable<SelectListItem> UnitSizeList { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
        public IEnumerable<SelectListItem> SupplierList { get; set; }
    }
}