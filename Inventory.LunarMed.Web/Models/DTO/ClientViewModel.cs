using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.LunarMed.Web.Models
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public bool IsSupplier { get; set; }
    }
}