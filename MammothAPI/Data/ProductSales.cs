using System;
using System.Collections.Generic;

namespace MammothAPI.Data
{
    public partial class ProductSales
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public DateTime BusinessDate { get; set; }
        public int SaleCount { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }

        public virtual Logins ModifiedByNavigation { get; set; }
        public virtual Products Product { get; set; }
        public virtual Stores Store { get; set; }
    }
}
