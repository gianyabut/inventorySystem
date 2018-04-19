using System;

namespace Inventory.LunarMed.Data.Entities.Base
{
    public abstract class BaseModel
    {
        public string UserCreated { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserModified { get; set; }

        public DateTime DateModified { get; set; }
    }
}
