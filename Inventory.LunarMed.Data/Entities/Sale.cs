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
    public class Sale : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public int Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string Remarks { get; set; }
        public string CustomerPONumber { get; set; }
        public int ClientId { get; set; }


        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        public ICollection<SaleDetails> OrderDetails { get; set; }
    }
}
