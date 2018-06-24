using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.Messages = new List<ViewMessage>();
        }

        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int Terms { get; set; }
        public string DueDate { get; set; }
        public string Remarks { get; set; }
        public string CustomerPONumber { get; set; }
        public string SalesInvoice { get; set; }
        public decimal Total { get; set; }
        public string Type { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }
        
        public IEnumerable<SelectListItem> ClientsList { get; set; }

        public List<ViewMessage> Messages { get; set; }
    }
}