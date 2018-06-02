using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class HomePageViewModel
    {
        public List<StockViewModel> Stocks { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public List<CollectionViewModel> Collections { get; set; }
    }
}