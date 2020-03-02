using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class Stores
    {
        public Stores()
        {
            ProductSalesModifiedByNavigation = new HashSet<ProductSales>();
            ProductSalesStore = new HashSet<ProductSales>();
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int GroupId { get; set; }
        public int LoginId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }

        public virtual StoreGroups Group { get; set; }
        public virtual Logins Login { get; set; }
        public virtual Users ModifiedByNavigation { get; set; }
        public virtual ICollection<ProductSales> ProductSalesModifiedByNavigation { get; set; }
        public virtual ICollection<ProductSales> ProductSalesStore { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
