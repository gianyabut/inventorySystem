using Inventory.LunarMed.Web.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models.Shared
{
    public class ResponseViewModel
    {
        public bool IsSuccessful { get; set; }
        public List<ViewMessage> Messages { get; set; }
    }
}