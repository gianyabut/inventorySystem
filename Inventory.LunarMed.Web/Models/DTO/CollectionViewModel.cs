using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.LunarMed.Web.Models
{
    public class CollectionViewModel
    {
        public int CollectionId { get; set; }

        public string CheckNumber { get; set; }
        public string TransactionNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }

        public string SalesInvoice { get; set; }

        public bool IsGovTax { get; set; }

        public IEnumerable<SelectListItem> SalesInvoiceList { get; set; }
    }
}