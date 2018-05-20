using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models
{
    public class CollectionViewModel
    {
        public int CollectionId { get; set; }

        public string CheckNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        public string CustomerPONumber { get; set; }

        public IEnumerable<SelectListItem> CustomerPONumberList { get; set; }
    }
}