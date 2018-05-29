using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int GenericId { get; set; }
        public string GenericName { get; set; }

        public IEnumerable<SelectListItem> GenericList { get; set; }
    }
}