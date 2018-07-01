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
    public class Order : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string Remarks { get; set; }
        public string CustomerPONumber { get; set; }
        public string SalesInvoice { get; set; }
        public int ClientId { get; set; }
        public decimal Total { get; set; }

        [Column(TypeName = "char")]
        [StringLength(1)]
        public string Type { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
