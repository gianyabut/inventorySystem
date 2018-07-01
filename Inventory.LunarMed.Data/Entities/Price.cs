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
    public class Price : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PriceId { get; set; }

        public int BrandId { get; set; }
        public decimal Cost { get; set; }
        public int UnitSizeId { get; set; }
        public int ClientId { get; set; }

        [ForeignKey("UnitSizeId")]
        public virtual UnitSize UnitSize { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
    }
}
