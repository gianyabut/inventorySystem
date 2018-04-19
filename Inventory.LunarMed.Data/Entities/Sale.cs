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
        public int SalesId { get; set; }


        [ForeignKey("Stock"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockId { get; set; }
        [ForeignKey("StockGroup"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StockGroupId { get; set; }

        public int Terms { get; set; }
        public DateTime DueDate { get; set; }
        public string Remarks { get; set; }
        public int Quantity { get; set; }
        public string CustomerPONumber { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual StockGroup StockGroup { get; set; }
    }
}
