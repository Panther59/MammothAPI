using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class ProductGroups
    {
        public ProductGroups()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual Logins LastModifiedByNavigation { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
