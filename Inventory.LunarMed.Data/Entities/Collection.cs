using Inventory.LunarMed.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.LunarMed.Data.Entities
{
    public class Collection : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionId { get; set; }

        public string CheckNumber { get; set; }
        public string TransactionNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        public int OrderId { get; set; }

        public bool IsGovTax { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
