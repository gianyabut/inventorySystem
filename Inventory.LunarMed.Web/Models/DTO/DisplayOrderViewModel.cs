using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class DisplayOrderViewModel
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Terms { get; set; }
        public string DueDate { get; set; }
        public string Remarks { get; set; }
        public string CustomerPONumber { get; set; }
        public decimal Total { get; set; }

        public List<DisplayOrderDetailsViewModel> OrderDetails { get; set; }
    }
}