using Inventory.LunarMed.Web.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models.Shared
{
    public class ViewMessage
    {
        public MessageType Type { get; set; }
        public string Message { get; set; }
    }
}