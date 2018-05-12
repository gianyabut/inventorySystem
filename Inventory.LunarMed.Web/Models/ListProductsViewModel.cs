﻿using Inventory.LunarMed.Web.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class ListProductsViewModel
    {
        public List<ProductViewModel> Stocks { get; set; }
        public List<ViewMessage> Messages { get; set; }
    }
}