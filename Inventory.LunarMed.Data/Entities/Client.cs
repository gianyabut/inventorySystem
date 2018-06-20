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
    public class Client : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public bool IsSupplier { get; set; }
    }
}
