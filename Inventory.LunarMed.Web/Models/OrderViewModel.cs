using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models
{
    public class OrderViewModel
    {
        public int SaleId { get; set; }
        public int ClientId { get; set; }
        public int Terms { get; set; }
        public string DueDate { get; set; }
        public string Remarks { get; set; }
        public string CustomerPONumber { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }


        public IEnumerable<SelectListItem> ClientsList { get; set; }
    }
}