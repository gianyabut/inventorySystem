using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class HomePageViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public List<OrderViewModel> Orders { get; set; }
        public List<CollectionViewModel> Collections { get; set; }
    }
}