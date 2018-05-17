using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class ListOrdersViewModel
    {
        public ListOrdersViewModel()
        {
            this.Messages = new List<ViewMessage>();
        }

        public List<OrderViewModel> Orders { get; set; }
        public List<ViewMessage> Messages { get; set; }
    }
}