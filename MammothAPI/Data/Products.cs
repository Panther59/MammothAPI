using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Products
    {
        public Products()
        {
            ProductSales = new HashSet<ProductSales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? GroupId { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int LastModifiedBy { get; set; }

        public virtual ProductGroups Group { get; set; }
        public virtual Logins LastModifiedByNavigation { get; set; }
        public virtual ICollection<ProductSales> ProductSales { get; set; }
    }
}
