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
    public class Stock : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }

        public int BrandId { get; set; }
        public string BatchNumber { get; set; }
        public decimal Cost { get; set; }
        public decimal SRP { get; set; }
        public decimal SRPDC { get; set; }
        public int MarkUp { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int UnitSizeId { get; set; }

        public string Supplier { get; set; }
        public DateTime PurchaseDate { get; set; }

        [ForeignKey("UnitSizeId")]
        public virtual UnitSize UnitSize { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

    }
}
